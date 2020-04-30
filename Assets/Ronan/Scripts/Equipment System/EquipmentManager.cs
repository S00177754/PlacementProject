using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject ActiveWeapon;
    public bool WeaponSheathed = true;

    [Header("Accessories")]
    public Bauble AccessorySlotOne;
    public Bauble AccessorySlotTwo;

    public GameObject UnsheathWeapon()
    {
        WeaponSheathed = false;
        return ActiveWeapon.gameObject;
    }

    public GameObject SheatheWeapon()
    {
        WeaponSheathed = true;
        return ActiveWeapon.gameObject;
    }
}
