using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AttackAnimation { SwordOutwardSlash, ThrustSlash, Melee360, HighSpin, JumpAttack, SpinDownSlash, SpinSwing, CrossSlash, BasicSlash,Shoot,ThrowSpell,DualCast,AOECast,AltDualCast }

public class PlayerAttack : MonoBehaviour
{
    [Header("Equipment")]
    public bool WeaponSheathed = true;
    public EquipmentManager Equipment;
    public EquipmentAttachController AttachPoints;

    [Header("Attack Details")]
    public float ComboTimeAllowance = 1f;
    public bool IsAttacking = false;
    public bool SlamAttack = false;
    public bool IsCharging = false;

    private float ChargeTimer = 0f;
    private bool CanCombo = false;
    public float ComboTimer = 0f;
    public int comboCounter = 0;
    private int ComboAttackIndex = 0;

    private float AttackCooldownTimer = 0f;
    public float CooldownTimer = 0f;

    private AttackInfoObj ActiveAttack;

    [HideInInspector]
    public int ComboAttackCount = 0;

    public AttackZoneManager ZoneManager;
    private AbilityTreeManager AbilityTree;
    
    
    [HideInInspector]
    public List<EnemyStatsScript> EnemiesToDamage = new List<EnemyStatsScript>();


    private void Start()
    {
        AbilityTree = GetComponent<AbilityTreeManager>();
        ZoneManager.GetAllAttackPatterns();
    }

    private void Update()
    {
        ChargeUpdate();
        ComboUpdate();
        AttackCooldown();

        if (!GetComponent<PlayerMovement>().IsFalling)
        {
            SlamAttack = false;
        }
        else
        {
            GetComponent<PlayerAnimator>().Animator.ResetTrigger("ChargeAttack");

            if (GetComponent<PlayerSurroundingDetection>().LandOnGroundCheck() && SlamAttack)
            {
                print("Slam DUNK");
                ActiveAttack = Equipment.GetAttackDetails().SlamAttack;

                ActivateAttackZone(ActiveAttack);
                DealMeleeDamage(ActiveAttack.DamageAmount);
                AttackCooldownTimer = CooldownTimer;
                comboCounter++;
                CanCombo = true;
                ComboTimer = 0f;
            }
        }
    }

    public void DisableAllAttackPatterns()
    {
        ZoneManager.DisableZones();
    }

    private void ActivateAttackZone(AttackInfoObj attackInfo)
    {
        DisableAllAttackPatterns();
        ZoneManager.ActivateZone(attackInfo.AttackZoneName);
    }

    #region Data Updates
    private void ComboUpdate()
    {
        if (!IsAttacking && !IsCharging)
        {
            if (CanCombo)
            {
                ComboTimer += Time.deltaTime;
            }
            else if(ComboTimer != 0)
            {
                ComboTimer = 0f;
            }  
        }

        if (ComboTimer >= ComboTimeAllowance)
        {
            CanCombo = false;
            ComboTimer = 0f;
            comboCounter = 0;
            ComboAttackIndex = 0;
        }
    }

    private void ChargeUpdate()
    {
        if (IsCharging)
        {
            ChargeTimer += Time.deltaTime;
        }
        else
        {
            ChargeTimer = 0;
            
        }
    }

    private void AttackCooldown()
    {
        if(AttackCooldownTimer > 0)
        {
            AttackCooldownTimer -= Time.deltaTime;
        }
        else
        {
            IsAttacking = false;
        }
    }
    #endregion

    #region Main Attack
    public void Charge()
    {
        if (!IsAttacking && !GetComponent<PlayerMovement>().IsCrouching)
        {
            GetComponent<PlayerAnimator>().Animator.ResetTrigger("CancelCharge");
            comboCounter = 0;
            ComboTimer = 0f;
            IsCharging = true;
            GetComponent<PlayerAnimator>().SetBool("IsCharging",true);
            ActiveAttack = Equipment.GetAttackDetails().ChargeAttack;
        }
    }

    public void CancelCharge()
    {
        IsCharging = false;
        GetComponent<PlayerAnimator>().SetInteger("AttackAnimation", -1);
        GetComponent<PlayerAnimator>().SetTrigger("CancelCharge");
    }

    public void Attack()
    {
        switch (Equipment.Loadout.EquippedWeapon.attackType)
        {
            case AttackType.Melee:
                MeleeAttack();
                break;

            case AttackType.Ranged:
                RangedAttack();
                break;
        }
    }

    #endregion

    #region Melee
    private void MeleeAttack()
    {
        if (!IsAttacking && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && !GetComponent<PlayerMovement>().IsFalling)
        {
            IsAttacking = true;

            if (!IsCharging)
            {
                ActiveAttack = Equipment.GetAttackDetails().PrimaryAtackPattern[ComboAttackIndex];

                PlayAttackAnimation(false, (int)ActiveAttack.Animation);
                ActivateAttackZone(ActiveAttack);
                DealMeleeDamage(ActiveAttack.DamageAmount);
                AttackCooldownTimer = CooldownTimer;

                ComboAttackIndex++;
                int abilityLimit = AbilityTree.GetMeleeComboLimit();
                if (ComboAttackIndex >= ComboAttackCount || ComboAttackIndex >= (abilityLimit + 1))
                {
                    ComboAttackIndex = 0;
                }

                comboCounter++;
                CanCombo = true;
                ComboTimer = 0f;
            }
            else
            {
                IsAttacking = false;
            }


        }
        else if (!IsAttacking && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && GetComponent<PlayerMovement>().IsFalling)
        {
            IsAttacking = true;
            SlamAttack = true;
            ActiveAttack = Equipment.GetAttackDetails().SlamAttack;
        }
    }

    private void MeleeCharge()
    {
        PlayAttackAnimation(true, (int)ActiveAttack.Animation);
        ActivateAttackZone(ActiveAttack);
        DealMeleeDamage(ActiveAttack.DamageAmount);
        AttackCooldownTimer = CooldownTimer;
        IsCharging = false;
        GetComponent<PlayerAnimator>().SetBool("IsCharging", false);
        ChargeTimer = 0;
        ComboTimer = 0f;
    }
    #endregion


    #region Ranged

    private void RangedAttack()
    {
        if (!IsAttacking && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && !GetComponent<PlayerMovement>().IsFalling)
        {
            IsAttacking = true;

            if (!IsCharging && GetNearestEnemy() != null)
            {
                ActiveAttack = Equipment.GetAttackDetails().PrimaryAtackPattern[ComboAttackIndex];

                PlayAttackAnimation(false, (int)ActiveAttack.Animation);
                EnemiesToDamage.Clear();
                EnemiesToDamage.Add(GetNearestEnemy());

                DealRangedDamage(ActiveAttack.DamageAmount);
                AttackCooldownTimer = CooldownTimer;

                ComboAttackIndex++;
                int abilityLimit = AbilityTree.GetRangedComboLimit();
                if (ComboAttackIndex >= ComboAttackCount || ComboAttackIndex >= (abilityLimit + 1))
                {
                    ComboAttackIndex = 0;
                }

                comboCounter++;
                CanCombo = true;
                ComboTimer = 0f;
            }
            else
            {
                IsAttacking = false;
                
            }


        }
        else if (!IsAttacking && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && GetComponent<PlayerMovement>().IsFalling)
        {
            IsAttacking = true;
            SlamAttack = true;
            ActiveAttack = Equipment.GetAttackDetails().SlamAttack; //RocketJump?
        }
    }

    private void RangedCharge()
    {
        if (GetNearestEnemy() != null)
        {
            PlayAttackAnimation(true, (int)ActiveAttack.Animation);
            EnemiesToDamage.Clear();
            EnemiesToDamage.Add(GetNearestEnemy());

            DealRangedDamage(ActiveAttack.DamageAmount);
            AttackCooldownTimer = CooldownTimer;
            IsCharging = false;
            GetComponent<PlayerAnimator>().SetBool("IsCharging", false);
            ChargeTimer = 0;
            ComboTimer = 0f;
        }
        else
        {
            GetComponent<PlayerAnimator>().Animator.SetTrigger("CancelCharge");
        }
    }


    private EnemyStatsScript GetNearestEnemy()
    {
        TargetableObject to = GetComponent<TargetManager>().FindNearestTarget();
        if (to != null)
        {
            return to.GetComponent<EnemyStatsScript>();
        }
        else return null;
    }


    #endregion

    #region Magic


    #endregion

    public void ChargeAttack(float duration)
    {
        if (ActiveAttack != null && IsCharging)
        {
            IsAttacking = true;
            ChargeTimer = duration;
            if (duration >= ActiveAttack.AttackCharge)
            {
                switch (Equipment.Loadout.EquippedWeapon.attackType)
                {
                    case AttackType.Melee:
                        MeleeCharge();
                        break;

                    case AttackType.Ranged:
                        RangedCharge();
                        break;
                }
            }
        }
    }

    public void AttackSlam()
    {
        if(ActiveAttack != null)
        {
            IsAttacking = true;
            ActivateAttackZone(ActiveAttack);
            DealMeleeDamage(ActiveAttack.DamageAmount);
        }
    }


    public void DealRangedDamage(int dmg)
    {
        if (AbilityTree.RangedTree.RootNode != null)
        {
            Debug.Log("Before Ability Tree: " + dmg);
            dmg += AbilityTree.GetRangedBonus();
            Debug.Log("After Ability Tree: " + dmg);
        }

        StartCoroutine(ApplyDamage(dmg));
        ActiveAttack = null;
    }

    public void DealMeleeDamage(int dmg)
    {
        if (AbilityTree.MeleeTree.RootNode != null)
        {
            Debug.Log("Before Ability Tree: " + dmg);
            dmg += AbilityTree.GetAttackBonus();
            Debug.Log("After Ability Tree: " + dmg);
        }

        StartCoroutine(ApplyDamage(dmg));
        ActiveAttack = null;
    }
    

    IEnumerator ApplyDamage(int damage)
    {
        yield return new WaitForSeconds(0.2f);
        EnemiesToDamage.ForEach(e => e.ApplyDamage(damage));
        EnemiesToDamage.Clear();
        DisableAllAttackPatterns();
    }

    public IEnumerator FreezeMovementFor(float time, bool movement, bool camera)
    {
        GetComponent<PlayerMovement>().SetFreeze(movement, camera);
        yield return new WaitForSeconds(time);
        GetComponent<PlayerMovement>().SetFreeze(false, false);
    }

    public float GetCooldownAmount()
    {
        //Debug.Log("Cooldown: "+ AttackCooldownTimer / CooldownTimer);
        return AttackCooldownTimer / CooldownTimer;
    }

    public float GetChargeAmount()
    {
        if(ActiveAttack != null)
        {
            //print("Charge: " + (ChargeTimer / ActiveAttack.AttackCharge));
            return ChargeTimer / ActiveAttack.AttackCharge;
        }

        return 0;
    }

    public void PlayAttackAnimation(bool charged,int attackAnimation)
    {
        if (!GetComponent<PlayerMovement>().IsCrouching)
        {
            GetComponent<PlayerAnimator>().SetTrigger(charged == true ? "ChargeAttack" : "Attack");
            GetComponent<PlayerAnimator>().SetInteger("AttackAnimation",attackAnimation);
        }
    }
}
