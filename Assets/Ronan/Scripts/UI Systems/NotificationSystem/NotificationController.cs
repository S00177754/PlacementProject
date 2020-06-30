using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationSprites { Item }

public class NotificationController : MonoBehaviour
{
    public RectTransform Content;
    public GameObject NotificationPanelPrefab;
    public Animator animator;

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void SendNotification(string message, Sprite sprite, Color color)
    {
        
        GameObject notification = Instantiate(NotificationPanelPrefab, Content);
        notification.GetComponent<NotificationElement>().SetNotification(message, sprite, color,animator);
        Debug.Log("Notified");
        animator.SetTrigger("Show");
    }
}
