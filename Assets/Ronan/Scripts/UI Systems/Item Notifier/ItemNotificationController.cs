using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotificationController : MonoBehaviour
{
    public TMP_Text ItemNamePanel;
    public Image ButtonIcon;

    public void SetItemName(string itemName)
    {
        ItemNamePanel.text = itemName;
    }
}
