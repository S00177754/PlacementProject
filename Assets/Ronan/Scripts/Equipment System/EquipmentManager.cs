using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject ActiveWeapon;


    //[Header("Accessories")]
    //public Bauble AccessorySlotOne;
    //public Bauble AccessorySlotTwo;

    public void EquipWeapon(GameObject weapon)
    {
        ActiveWeapon = weapon;
    }


}
