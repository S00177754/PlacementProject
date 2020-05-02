using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Weapon, Bauble, Ammo, Potion, KeyItem  }

//Base item class
public abstract class ItemObj : ScriptableObject
{
    public string Name;
    public ItemType Type;

    [TextArea(10, 15)] //Came across on Unity forms
    public string Description;
    
    public GameObject ItemPrefab;
}












