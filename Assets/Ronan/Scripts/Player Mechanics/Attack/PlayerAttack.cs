using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float MaxChargeTime = 5f;
    private float ChargeTimer = 0f;

    private bool CanCombo = false;
    public float ComboTimer = 0f;
    public int comboCounter = 0;
    private int ComboAttackIndex = 0;

    [HideInInspector]
    public int ComboAttackCount = 0;

    private Dictionary<string, AttackDamageZone> Zones = new Dictionary<string, AttackDamageZone>();
    

    private void Start()
    {
        GetAllAttackPatterns();
        SheathWeapon();
       
    }

    private void Update()
    {
        //if (!IsAttacking)
        //{
        //    DisableAllAttackPatterns();
        //}

        ComboUpdate();
    }

    #region Input Methods

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if (!GetComponent<PlayerMovement>().FreezeMovement)
        {

            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && !GetComponent<PlayerMovement>().IsFalling)
                    {
                        WeaponSheathed = false;
                        //UnsheathWeapon();
                    }
                    else if (!WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching)
                    {
                       //Equipment.ActiveWeapon.GetComponent<WeaponInfo>().Attack();
                        GetComponent<PlayerAnimator>().SwitchTo(PlayerAnimation.Slash);
                    }
                    break;
            }
        }
    }

    public void OnSneak(InputAction.CallbackContext context)
    {
        if (!GetComponent<PlayerMovement>().FreezeMovement)
        {
            if (!WeaponSheathed)
            {
                WeaponSheathed = true;
            }
        }

    }

    #endregion


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
        }
    }

    private void DisableAllAttackPatterns()
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
            //zone.gameObject.SetActive(true);
            zone.DealDamage(attackInfo.DamageAmount);
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

    public void Charge()
    {
        if (!WeaponSheathed)
        {
            ComboTimer = 0f;
            IsCharging = true;
        }
    }

    public void Attack()
    {
        if (!IsAttacking && !WeaponSheathed) 
        {
            IsAttacking = true;

            if (IsCharging)
            {
                if (!WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching)
                {
                    GetComponent<PlayerAnimator>().SwitchTo(PlayerAnimation.Slash);
                }

                ActivateAttackZone(Equipment.GetAttackDetails().ChargeAttack);
                IsCharging = false;
                ComboAttackIndex = 0;
                Debug.Log("Charge Attack");
            }
            else
            {
                ActivateAttackZone(Equipment.GetAttackDetails().PrimaryAtackPattern[ComboAttackIndex]);

                if (!WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching)
                {
                    GetComponent<PlayerAnimator>().SwitchTo(PlayerAnimation.Slash);
                }


                Debug.Log(ComboAttackIndex);
                ComboAttackIndex++;
                if (ComboAttackIndex >= ComboAttackCount)
                {
                    ComboAttackIndex = 0;
                }

                comboCounter++;
                CanCombo = true;
            }
                ComboTimer = 0f;


            IsAttacking = false;
        }

        if (WeaponSheathed && !GetComponent<PlayerMovement>().IsCrouching && !GetComponent<PlayerMovement>().IsJumping && !GetComponent<PlayerMovement>().IsFalling)
        {
            WeaponSheathed = false;
        }

    }
}
