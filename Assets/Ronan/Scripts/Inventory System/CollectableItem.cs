using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public ItemObj Item;

    public string GetItemName()
    {
        return Item.Name;
    }
}
