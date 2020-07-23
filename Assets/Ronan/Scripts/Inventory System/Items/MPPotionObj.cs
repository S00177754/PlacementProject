using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MP Potion Object", menuName = "Inventory System/Item/Potions/MP")]
public class MPPotionObj : ItemObj
{
    public int RestoreAmount;
    public bool AffectParty;

    public void Awake()
    {
        Type = ItemType.Potion;
    }
}

