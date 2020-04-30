using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public EquipmentManager Equipment;
    public EquipmentAttachController Attacher;



    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                
                if (Equipment.WeaponSheathed)
                {
                    Attacher.AttatchTo(AttachPoint.RightHand,Equipment.UnsheathWeapon().transform);
                }
                
                break;
        }
    }

    public void SheatheWeapon(InputAction.CallbackContext context)
    {
        if (!Equipment.WeaponSheathed)
        {
            Attacher.AttatchTo(AttachPoint.LeftHip, Equipment.SheatheWeapon().transform);
        }

    }
}
