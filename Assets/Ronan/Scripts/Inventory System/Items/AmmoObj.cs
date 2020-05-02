using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Object", menuName = "Inventory System/Item/Ammo")]
public class AmmoObj : ItemObj
{
    public int AmmoAmount;

    public void Awake()
    {
        Type = ItemType.Ammo;
    }
}