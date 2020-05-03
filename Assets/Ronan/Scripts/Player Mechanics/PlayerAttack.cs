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
        SheathWeapon();
    }

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
