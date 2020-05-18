﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public Image SpriteIcon;
    public TMP_Text ItemName;
    public TMP_Text AmountDisplay;

    [Header("Item Scriptable Object")]
    public ItemObj Item;
    public InventoryPanelController InventoryPanel;

    public void SetDetails(InventorySlot inventorySlot,InventoryPanelController inventoryPanel)
    {
        Item = inventorySlot.Item;
        AmountDisplay.text = "X " + inventorySlot.Amount;
        ItemName.text = Item.Name;
        InventoryPanel = inventoryPanel;
    }

    public void UseInventoryItem()
    {
        if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Party.Count == 0)
        {
            if (Item.UseItem(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer))
            {
                InventoryPanel.PlayerInventory.Inventory.RemoveItem(Item, 1);
                InventoryPanel.GenerateList();

                //Animation call here

                GameStateController.ResumePreviousState();
            }
        }
    }

    
}