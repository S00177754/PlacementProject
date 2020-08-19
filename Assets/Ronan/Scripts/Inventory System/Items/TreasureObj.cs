using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Treasure Object", menuName = "Inventory System/Item/Treasure")]
public class TreasureObj : ItemObj
{
    public void Awake()
    {
        Type = ItemType.Treasure;
    }
}
