using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inevntory Object", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject
{
    public List<InventorySlot> Collection = new List<InventorySlot>();

    public void AddItem(ItemObj item, int amount)
    {
        for (int i = 0; i < Collection.Count; i++)
        {
            if(Collection[i].Item == item)
            {
                Collection[i].AddAmount(amount);
                return;
            }
        }

        Collection.Add(new InventorySlot(item, amount));
    }
     
    public void RemoveItem(ItemObj item, int amount)
    {
        for (int i = 0; i < Collection.Count; i++)
        {
            if (Collection[i].Item == item)
            {
                Collection[i].RemoveAmount(amount);

                if(Collection[i].Amount == 0)
                {
                    Collection.RemoveAt(i);
                }
                return;
            }
        }
    }

}

[Serializable]
public class InventorySlot
{
    public ItemObj Item;
    public int Amount;

    public InventorySlot(ItemObj item, int amount)
    {
        Item = item;
        Amount = amount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public bool RemoveAmount(int amount)
    {
        if(amount <= Amount)
        {
            Amount -= amount;
            return true;
        }

        return false;
    }
}
