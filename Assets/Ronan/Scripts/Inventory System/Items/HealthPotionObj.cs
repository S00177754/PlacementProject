using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion Object", menuName = "Inventory System/Item/Potions/Health")]
public class HealthPotionObj : ItemObj
{
    public int HealAmount;
    public bool AffectParty;

    public void Awake()
    {
        Type = ItemType.Potion;
    }
}
