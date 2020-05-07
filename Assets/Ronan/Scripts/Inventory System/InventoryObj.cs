using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inevntory Object", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject, ISerializationCallbackReceiver
{
    //Will move at a later point
    public string SaveLocation;

    public ItemDatabaseObject database;
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

        Collection.Add(new InventorySlot(database.GetId[item],item, amount));
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Collection.Count; i++)
        {
            Collection[i].Item = database.GetItem[Collection[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void Save()
    {
        string data = JsonUtility.ToJson(this, true);
        BinaryFormatter Binary = new BinaryFormatter();
        FileStream fs = File.Create(string.Concat(Application.persistentDataPath, SaveLocation));
        Binary.Serialize(fs, data);
        fs.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, SaveLocation)))
        {
            BinaryFormatter Binary = new BinaryFormatter();
            FileStream fs = File.Open(string.Concat(Application.persistentDataPath, SaveLocation), FileMode.Open);
            JsonUtility.FromJsonOverwrite(Binary.Deserialize(fs).ToString(), this);
            fs.Close();
        }
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
    public int ID;
    public ItemObj Item;
    public int Amount;

    public InventorySlot(int id, ItemObj item, int amount)
    {
        ID = id;
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
