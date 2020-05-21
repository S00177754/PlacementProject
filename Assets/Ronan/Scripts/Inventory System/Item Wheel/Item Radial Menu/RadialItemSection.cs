using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialItemSection : RadialMenuSection
{
    public ItemObj Item;

    public void SetItem(ItemObj obj) 
    {
        Item = obj;
        IconImage.sprite = obj.ItemIcon;
    }

    public void UseItem()
    {

    }

    
}
