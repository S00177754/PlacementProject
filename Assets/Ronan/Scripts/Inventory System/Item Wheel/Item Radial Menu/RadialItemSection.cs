using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialItemSection : RadialMenuSection
{
    public ItemObj Item;

    public override void Start()
    {
        base.Start();

        if (Item != null)
        {
            IconImage.sprite = Item.ItemIcon;
        }

    }

    public void SetItem(ItemObj obj) 
    {
        Item = obj;
        IconImage.sprite = obj.ItemIcon;
    }

    public override void HighlightSection(Color highlightColor)
    {
        if((Controller as ItemRadialMenuController).InventoryManager.Inventory.GetItemRemainingAmount(Item) <= 0)
        {
            PanelBackground.color = Color.red;
        }
        else
        {
            PanelBackground.color = highlightColor;
        }

        //base.HighlightSection(highlightColor);
    }

    public void UseItem(InventoryManager inventoryManager)
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Party.Count == 0)
        {
            if ((Controller as ItemRadialMenuController).InventoryManager.Inventory.GetItemRemainingAmount(Item) > 0)
            {
                if (Item.UseItem(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer))
                {
                    inventoryManager.Inventory.RemoveItem(Item, 1);
                    //Animation
                }
            }
        }
    }

    
}
