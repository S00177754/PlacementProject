using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bauble Object", menuName = "Inventory System/Item/Bauble")]
public class BaubleObj : ItemObj
{
    public float AttackBonus;
    public float DefenseBonus;
    public float MaxHealthBonus;
    public float MaxMPBonus;

    public void Awake()
    {
        Type = ItemType.Bauble;
    }
}