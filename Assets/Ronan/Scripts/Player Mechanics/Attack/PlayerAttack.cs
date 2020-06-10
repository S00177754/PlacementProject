using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AttackAnimation { SwordOutwardSlash, ThrustSlash, Melee360, HighSpin, JumpAttack, SpinDownSlash, SpinSwing, CrossSlash, BasicSlash}

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

    //Not implemented yet, still undecided
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
    
    
    [HideInInspector]
    public List<EnemyStatsScript> EnemiesToDamage = new List<EnemyStatsScript>();


    private void Start()
    {
        ZoneManager.GetAllAttackPatterns();
        SheathWeapon();
       
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
    }


    #region Sheath Weapon Methods

    public void SheathWeapon()
    {
        
        AttachPoints.AttatchTo(AttachPoint.LeftHip, Equipment.ActiveWeapon);
    }

    public void UnsheathWeapon()
    {
        
        AttachPoints.AttatchTo(AttachPoint.RightHand, Equipment.ActiveWeapon);
    }

    #endregion

 

    public void DisableAllAttackPatterns()
    {
        ZoneManager.DisableZones();
    }

    private void ActivateAttackZone(AttackInfoObj attackInfo)
    {
        DisableAllAttackPatterns();

        ZoneManager.ActivateZone(attackInfo.AttackZoneName);

    }

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

    public void Charge()
    {
        if (!IsAttacking)
        {
            comboCounter = 0;
            ComboTimer = 0f;
            IsCharging = true;
            ActiveAttack = Equipment.GetAttackDetails().ChargeAttack;
        }
    }

    public void Attack()
    {
        if (!IsAttacking && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && !GetComponent<PlayerMovement>().IsFalling)
        {
            IsAttacking = true;

            if (!IsCharging)
            {
                ActiveAttack = Equipment.GetAttackDetails().PrimaryAtackPattern[ComboAttackIndex];

                //Animation stuff bro
                //StartCoroutine(ApplyDamage(ActiveAttack.DamageAmount));
                ActivateAttackZone(ActiveAttack);
                DealDamage(ActiveAttack.DamageAmount);
                AttackCooldownTimer = CooldownTimer;
                //IsAttacking = false;

                ComboAttackIndex++;
                if (ComboAttackIndex >= ComboAttackCount)
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
        else if(!IsAttacking && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && GetComponent<PlayerMovement>().IsFalling)
        {
            IsAttacking = true;
            SlamAttack = true;
            ActiveAttack = Equipment.GetAttackDetails().SlamAttack;
        }

        #region old code
        //if (!IsAttacking && !WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && !GetComponent<PlayerMovement>().IsFalling) 
        //{
        //    IsAttacking = true;

        //    if (IsCharging)
        //    {
        //        GetComponent<PlayerMovement>().SetFreeze(false, false);
        //        AttackInfoObj attackInfo = Equipment.GetAttackDetails().ChargeAttack;
        //        ActivateAttackZone(attackInfo);

        //        if (!WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching)
        //        {

        //            GetComponent<PlayerAnimator>().SetTrigger("ChargeAttack");
        //            GetComponent<PlayerAnimator>().SetInteger("AttackAnimation", (int)Equipment.GetAttackDetails().ChargeAttack.Animation);
        //        }

        //        StartCoroutine(ApplyDamage(attackInfo.DamageAmount));

        //        IsCharging = false;
        //        ComboAttackIndex = 0;
        //    }
        //    else
        //    {
        //        AttackInfoObj attackInfo = Equipment.GetAttackDetails().PrimaryAtackPattern[ComboAttackIndex];
        //        ActivateAttackZone(attackInfo);

        //        if (!WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching)
        //        {
        //            GetComponent<PlayerAnimator>().SetTrigger("Attack");
        //            GetComponent<PlayerAnimator>().SetInteger("AttackAnimation", (int)Equipment.GetAttackDetails().PrimaryAtackPattern[ComboAttackIndex].Animation);
        //        }



        //        //Debug.Log(attackInfo);
        //        ComboAttackIndex++;
        //        if (ComboAttackIndex >= ComboAttackCount)
        //        {
        //            ComboAttackIndex = 0;
        //        }

        //        comboCounter++;
        //        CanCombo = true;

        //        StartCoroutine(ApplyDamage(attackInfo.DamageAmount));


        //    }
        //        ComboTimer = 0f;



        //}

        //if (WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && !GetComponent<PlayerMovement>().IsFalling)
        //{
        //    StartCoroutine(GetComponentInParent<PlayerAttack>().FreezeMovementFor(1.2f, true, false));
        //    WeaponSheathed = false;
        //}
        #endregion
    }

    public void ChargeAttack(float duration)
    {
        if (ActiveAttack != null && IsCharging)
        {
            IsAttacking = true;
            ChargeTimer = duration;
            if (duration >= ActiveAttack.AttackCharge)
            {
                //Animation stuff bro
                //StartCoroutine(ApplyDamage(ActiveAttack.DamageAmount));
                ActivateAttackZone(ActiveAttack);
                DealDamage(ActiveAttack.DamageAmount);
                AttackCooldownTimer = CooldownTimer;
                IsCharging = false;
                ChargeTimer = 0;
                ComboTimer = 0f;
            }
        }
    }

    public void AttackSlam()
    {
        if(ActiveAttack != null)
        {
            IsAttacking = true;
            ActivateAttackZone(ActiveAttack);
            DealDamage(ActiveAttack.DamageAmount);
        }
    }


    public void DealDamage(int dmg)
    {
        //EnemiesToDamage.ForEach(e => e.ApplyDamage(dmg));
        //EnemiesToDamage.Clear();

        //DisableAllAttackPatterns();
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
}
