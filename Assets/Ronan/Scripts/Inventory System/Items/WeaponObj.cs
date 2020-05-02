using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Object", menuName = "Inventory System/Item/Weapon")]
public class WeaponObj : ItemObj
{
    public int AttackPower;

    public void Awake()
    {
        Type = ItemType.Weapon;
    }
}