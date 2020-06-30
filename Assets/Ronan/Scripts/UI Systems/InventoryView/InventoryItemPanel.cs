using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemPanel : MonoBehaviour, ISelectHandler
{
    [Header("UI Elements")]
    //public Image SpriteIcon;
    public TMP_Text ItemName;
    public TMP_Text AmountDisplay;

    [Header("Item Scriptable Object")]
    public ItemObj Item;
    public InventorySystemController InventoryPanel;

    

    public void SetDetails(InventorySlot inventorySlot,InventorySystemController inventoryPanel)
    {
        Item = inventorySlot.Item;
        AmountDisplay.text = "X " + inventorySlot.Amount;
        ItemName.text = Item.Name;
        InventoryPanel = inventoryPanel;
    }

    public void UseInventoryItem()
    {
        InventoryPanel.ItemListActive = false;
        InventoryPanel.ItemUsageMenu.SetActive(true);
        InventoryPanel.ItemUsageMenu.transform.position = new Vector3(InventoryPanel.ItemUsageMenu.transform.position.x, transform.position.y, 0);
        InventoryPanel.ItemUsageMenu.GetComponent<InventoryItemUsagePanel>().Activate(this);
        InventoryPanel.ItemUsageMenu.GetComponent<InventoryItemUsagePanel>().FocusOn();
    }


    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log("Selected");
        InventoryPanel.DescriptionPanel.SetName(Item.Name);
        InventoryPanel.DescriptionPanel.SetDescription(Item.GetItemDetailText());
    }

   
}
