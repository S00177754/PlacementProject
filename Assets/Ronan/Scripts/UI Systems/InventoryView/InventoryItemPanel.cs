using System.Collections;
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
        InventoryPanel.ItemPanelMenu.SetActive(true);
        InventoryPanel.ItemPanelMenu.transform.position = new Vector3(InventoryPanel.ItemPanelMenu.transform.position.x, transform.position.y, 0);
        InventoryPanel.ItemPanelMenu.GetComponent<InventoryItemUsagePanel>().Activate(this);
        InventoryPanel.ItemPanelMenu.GetComponent<InventoryItemUsagePanel>().FocusOn();
    }

    
}
