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

    public void SetDetails(InventorySlot inventorySlot)
    {
        Item = inventorySlot.Item;
        AmountDisplay.text = "X " + inventorySlot.Amount;
        ItemName.text = Item.Name;
        //SpriteIcon.sprite = itemIcon;

    }
}
