using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to refactor item type to include all weapons
public enum ItemType { Potion,Weapon,  Bauble, KeyItem, Treasure  }

//Base item class
public abstract class ItemObj : ScriptableObject
{
    public int ID;
    public string Name;
    public ItemType Type;
    public Sprite ItemIcon;

    [TextArea(10, 15)] //Came across on Unity forms
    public string ItemWheelDescription;

    [TextArea(10, 15)] //Came across on Unity forms
    public string Description;

    [Header("Merchant Related")]
    public int BuyPrice;
    public int SellPrice;
    
    public virtual bool UseItem(PlayerController player)
    {
        return false;
    }

    public virtual bool UseItem(PartyMember member)
    {
        return false;
    }

    /// <summary>
    /// Returns formatted text based on items details
    /// </summary>
    /// <returns></returns>
    public virtual string GetItemDetailText()
    {
        return null;
    }
}












