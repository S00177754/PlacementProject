using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Weapon")]
    public WeaponInfo ActiveWeapon;

    private void Start()
    {
        GetComponent<PlayerAttack>().ComboAttackCount = GetAttackDetails().PrimaryAtackPattern.Count;
    }

    //[Header("Accessories")]
    //public Bauble AccessorySlotOne;
    //public Bauble AccessorySlotTwo;

    public void EquipWeapon(WeaponInfo weapon)
    {
        ActiveWeapon = weapon;
    }

    public WeaponAttackDetailsObj GetAttackDetails()
    {
        if (ActiveWeapon != null)
        {
            return ActiveWeapon.AttackDetails;
        }
        else return null;

    }

}
