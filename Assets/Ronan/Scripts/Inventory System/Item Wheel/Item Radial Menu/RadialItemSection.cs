using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialItemSection : RadialMenuSection
{
    //*************** Public Variables ********************
    public ItemObj Item;
    public ItemRadialMenuController ItemRadialMenu;
    public int Amount;

    //**************** Monobehaviour Methods ******************
    public override void Start()
    {
        base.Start();
        SetIconAlpha(0);

        if (Item != null)
        {
            SetIconAlpha(1);
            IconImage.sprite = Item.ItemIcon;
        }

    }

    //**************** Functionality Methods ****************
    public void SetItem(ItemObj obj, InventoryManager manager) 
    {
        SetIconAlpha(1);

        Item = obj;
        IconImage.sprite = obj.ItemIcon;
        Amount = manager.Inventory.Collection.Where(x => x.Item == obj).SingleOrDefault() == null ? 0: manager.Inventory.Collection.Where(x => x.Item == obj).SingleOrDefault().Amount;
    }

    public void UseItem(InventoryManager inventoryManager)
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Party.Count == 0)
        {
            if (PlayerController.Instance.GetComponent<InventoryManager>().Inventory.GetItemRemainingAmount(Item) > 0)
            {
                //if (Item.UseItem(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer))
                if (Item.UseItem(PlayerController.Instance))
                {
                    PlayerController.Instance.GetComponent<InventoryManager>().Inventory.RemoveItem(Item, 1);
                    //Animation
                }
            }
        }
    }


    //**************** Initialisation & Close Methods *********************
    public override void SetupComponents()
    {
        base.SetupComponents();
    }


    //**************** Graphical Methods *********************
    public override void HighlightSection(Color unrestricted, Color restricted)
    {
        if (Item != null)
        {
            //ItemRadialMenu.InfoPanel.SetInfoPanel(Item.Name, Item.Description, Amount);
            GetComponentInParent<ItemRadialMenuController>().InfoPanel.SetInfoPanel(Item.Name, Item.Description, Amount);

            if (PlayerController.Instance.GetComponent<InventoryManager>().Inventory.GetItemRemainingAmount(Item) <= 0)
            {
                PanelBackground.color = restricted;
            }
            else
            {
                PanelBackground.color = unrestricted;
            }
        }
        else
        {
            GetComponentInParent<ItemRadialMenuController>().InfoPanel.SetInfoPanel("", "", 0);
            PanelBackground.color = restricted;
        }

        
    }

    
}
