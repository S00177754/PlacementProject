using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key Item Object", menuName = "Inventory System/Item/Key Item")]
public class KeyItemObj : ItemObj
{
    public void Awake()
    {
        Type = ItemType.KeyItem;
    }
}
