using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to refactor item type to include all weapons
public enum ItemType {Weapon, Potion, Bauble, Ammo,  KeyItem  }
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












