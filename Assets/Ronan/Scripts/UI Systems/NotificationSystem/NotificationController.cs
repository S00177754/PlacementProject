using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationSprites { Item }

public class NotificationController : MonoBehaviour
{
    public RectTransform Content;
    public GameObject NotificationPanelPrefab;

    public void SendNotification(string message, Sprite sprite, Color color)
    {
        GameObject notification = Instantiate(NotificationPanelPrefab, Content);
        notification.GetComponent<NotificationElement>().SetNotification(message, sprite, color);
    }
}
