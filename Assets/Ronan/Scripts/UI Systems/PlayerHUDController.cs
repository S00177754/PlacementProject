using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    public ItemNotificationController ItemPanel;

    private void Start()
    {
        HideItemNotification();
    }

    public void SetupItemNotification(string itemName)
    {
        ItemPanel.gameObject.SetActive(true);
        ItemPanel.SetItemName(itemName);
    }

    public void HideItemNotification()
    {
        ItemPanel.gameObject.SetActive(false);
    }
}
