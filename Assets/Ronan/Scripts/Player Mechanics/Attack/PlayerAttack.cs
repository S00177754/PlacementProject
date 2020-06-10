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
    public GameObject AttackZones;
    public float ComboTimeAllowance = 1f;
    public bool IsAttacking = false;

    public bool IsCharging = false;

    //Not implemented yet, still undecided
    public float MaxChargeTime = 3f;
    private float ChargeTimer = 0f;

    private bool CanCombo = false;
    public float ComboTimer = 0f;
    public int comboCounter = 0;
    private int ComboAttackIndex = 0;
    private float AttackCooldownTimer = 0f;
    private AttackInfoObj ActiveAttack;

    [HideInInspector]
    public int ComboAttackCount = 0;

    private Dictionary<string, AttackDamageZone> Zones = new Dictionary<string, AttackDamageZone>();
    
    [HideInInspector]
    public List<EnemyStatsScript> EnemiesToDamage = new List<EnemyStatsScript>();


    private void Start()
    {
        GetAllAttackPatterns();
        SheathWeapon();
       
    }

    private void Update()
    {
        ChargeUpdate();
        ComboUpdate();
        AttackCooldown();
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

    private void GetAllAttackPatterns()
    {
        AttackDamageZone[] components = AttackZones.GetComponentsInChildren<AttackDamageZone>(true);
        for (int i = 0; i < components.Length; i++)
        {
            Zones.Add(components[i].ZoneName, components[i]);
            components[i].SetPlayer(this);
        }
    }

    public void DisableAllAttackPatterns()
    {
        foreach (KeyValuePair<string, AttackDamageZone> item in Zones)
        {
            item.Value.gameObject.SetActive(false);
        }

    }

    private void ActivateAttackZone(AttackInfoObj attackInfo)
    {
        DisableAllAttackPatterns();

        AttackDamageZone zone;
        if(Zones.TryGetValue(attackInfo.AttackZoneName, out zone))
        {
            zone.Activate();
        }
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
                DealDamage(ActiveAttack.DamageAmount);
                //StartCoroutine(ApplyDamage(ActiveAttack.DamageAmount));
                ActivateAttackZone(ActiveAttack);
                AttackCooldownTimer = ActiveAttack.AttackCooldown;
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

    public void ChargeAttack()
    {
        if (ActiveAttack != null && IsCharging)
        {
            if (ChargeTimer >= ActiveAttack.AttackCharge)
            {
                //Animation stuff bro
                DealDamage(ActiveAttack.DamageAmount);
                //StartCoroutine(ApplyDamage(ActiveAttack.DamageAmount));
                ActivateAttackZone(ActiveAttack);
                AttackCooldownTimer = ActiveAttack.AttackCooldown;
                IsCharging = false;
                ChargeTimer = 0;
                ComboTimer = 0f;
            }
        }
    }


    public void DealDamage(int dmg)
    {
        EnemiesToDamage.ForEach(e => e.ApplyDamage(dmg));
        EnemiesToDamage.Clear();
        DisableAllAttackPatterns();
    }
    

    IEnumerator ApplyDamage(int damage)
    {
        GetComponent<PlayerMovement>().SetFreeze(true, false);
        //yield return new WaitForSeconds(1.7f);
        EnemiesToDamage.ForEach(e => e.ApplyDamage(damage));
        EnemiesToDamage.Clear();
        DisableAllAttackPatterns();
        GetComponent<PlayerMovement>().SetFreeze(false, false);
        yield return new WaitForSeconds(0.2f);
        //IsAttacking = false;
    }

    public IEnumerator FreezeMovementFor(float time, bool movement, bool camera)
    {
        GetComponent<PlayerMovement>().SetFreeze(movement, camera);
        yield return new WaitForSeconds(time);
        GetComponent<PlayerMovement>().SetFreeze(false, false);
    }
}
