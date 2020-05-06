using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to refactor item type to include all weapons
public enum ItemType {Weapon, Potion, Bauble, KeyItem  }

//Base item class
public abstract class ItemObj : ScriptableObject
{
    public string Name;
    public ItemType Type;

    [TextArea(10, 15)] //Came across on Unity forms
    public string Description;
    
    public virtual bool UseItem(PlayerController player)
    {
        return false;
    }

    public virtual bool UseItem(PartyMember member)
    {
        return false;
    }
}












