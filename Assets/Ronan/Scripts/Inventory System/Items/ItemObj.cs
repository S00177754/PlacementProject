using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Weapon, Bauble, Ammo, Potion, KeyItem  }
public enum InventorySection { Equipment, Potions, KeyItems }

//Base item class
public abstract class ItemObj : ScriptableObject
{
    public string Name;
    public ItemType Type;
    public InventorySection Section;

    [TextArea(10, 15)] //Came across on Unity forms
    public string Description;
    
    public GameObject ItemPrefab;
}












