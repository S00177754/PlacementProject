using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    public ItemNotificationController ItemPanel;
    public NotificationController NotificationController;
    public RadialMenuController ItemWheel;

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

    public bool IsItemwheelActive()
    {
        return ItemWheel.gameObject.activeSelf;
    }

    public void ShowItemWheel(bool value)
    {
        ItemWheel.gameObject.SetActive(value);
        ItemWheel.SetInputAxis(Vector2.zero);
    }
}
