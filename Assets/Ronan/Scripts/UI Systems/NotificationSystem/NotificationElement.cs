using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationElement : MonoBehaviour
{
    public Image NotificationPanel;
    public Image NotificationIcon;
    public TMP_Text Text;

    private void Start()
    {
        Invoke("Destroy", 2);
    }

    public void SetNotification(string text, Sprite icon, Color panelColor)
    {
        Text.text = text;
        NotificationIcon.sprite = icon;

        Color color = panelColor;
        color.a = 0.6f;
        NotificationPanel.color = color;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}
