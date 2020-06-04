using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    public ItemNotificationController ItemPanel;
    public NotificationController NotificationController;

    public ItemRadialMenuController ItemWheel;

    public float ItemMenuCooldown = 1f;
    //Add in cooldown so menu cant be spammed, then work on action usage

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

    public void SendItemNotification(string message, Sprite sprite, Color color)
    {
        NotificationController.SendNotification(message, sprite, color);
    }

    public ItemRadialMenuController ActivateRadialMenu()
    {
        ItemWheel.gameObject.SetActive(true);
        Time.timeScale = 0.01f;
        ItemWheel.Startup();
        return ItemWheel;
    }

    public void CloseRadialMenu()
    {
        Time.timeScale = 1f;
        ItemWheel.CloseMenu();
    }

}
