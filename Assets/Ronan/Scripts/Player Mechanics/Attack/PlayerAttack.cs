using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public bool WeaponSheathed = true;
    public EquipmentManager Equipment;
    public EquipmentAttachController AttachPoints;

    public GameObject AttackZones;
    private Dictionary<string, AttackDamageZone> Zones = new Dictionary<string, AttackDamageZone>();

    private void Start()
    {
        GetAllAttackPatterns();
        SheathWeapon();
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
                        Equipment.ActiveWeapon.GetComponent<WeaponInfo>().Attack();
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
        AttachPoints.AttatchTo(AttachPoint.LeftHip, Equipment.ActiveWeapon.GetComponent<WeaponInfo>());
    }

    public void UnsheathWeapon()
    {
        AttachPoints.AttatchTo(AttachPoint.RightHand, Equipment.ActiveWeapon.GetComponent<WeaponInfo>());
    }

    #endregion

    public void GetAllAttackPatterns()
    {
        AttackDamageZone[] components = AttackZones.GetComponentsInChildren<AttackDamageZone>();
        for (int i = 0; i < components.Length; i++)
        {
            Zones.Add(components[i].ZoneName, components[i]);
        }
    }

    public void Attack()
    {

    }
}
