using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public bool WeaponSheathed = true;
    public EquipmentManager Equipment;
    public EquipmentAttachController AttachPoints;

    private void Start()
    {
        
    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (WeaponSheathed)
                {
                    WeaponSheathed = false;
                    UnsheathWeapon();
                }
                else
                {
                    Equipment.ActiveWeapon.GetComponent<WeaponInfo>().Attack();
                }
                break;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (!WeaponSheathed)
        {
            WeaponSheathed = true;
            SheathWeapon();
        }

    }

    public void SheathWeapon()
    {
        AttachPoints.AttatchTo(AttachPoint.LeftHip, Equipment.ActiveWeapon.GetComponent<WeaponInfo>());
    }

    public void UnsheathWeapon()
    {
        AttachPoints.AttatchTo(AttachPoint.RightHand, Equipment.ActiveWeapon.GetComponent<WeaponInfo>());
    }

    public void Attack()
    {

    }
}
