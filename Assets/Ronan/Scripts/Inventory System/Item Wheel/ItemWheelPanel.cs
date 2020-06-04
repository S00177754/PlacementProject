using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWheelPanel : MonoBehaviour
{
    public TMP_Text ItemNameField;
    public TMP_Text ItemDescriptionField;
    public TMP_Text ItemAmountField;

    public void SetInfoPanel(string itemName, string itemDescription, int amount)
    {
        ItemNameField.text = itemName;
        ItemDescriptionField.text = itemDescription;
        ItemAmountField.text = string.Concat("Remaining: X", amount);
    }
}
