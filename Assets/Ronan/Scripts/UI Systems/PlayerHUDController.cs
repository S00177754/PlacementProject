using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    public PlayerController Player;

    public ItemNotificationController ItemPanel;
    public NotificationController NotificationController;

    public ItemRadialMenuController ItemWheel;

    public Image Cooldown;
    public Image Charge;

    public float ItemMenuCooldown = 1f;
    //Add in cooldown so menu cant be spammed, then work on action usage

    private void Start()
    {
        HideItemNotification();
    }

    private void Update()
    {
        Cooldown.fillAmount = Player.GetComponent<PlayerAttack>().GetCooldownAmount();
        Charge.fillAmount = Player.GetComponent<PlayerAttack>().GetChargeAmount();
    }

    public void SetupItemNotification(string itemName)
    {
        ItemPanel.gameObject.SetActive(true);
        ItemPanel.SetItemName(itemName);
    }

    public void SetupNotification(string message)
    {
        ItemPanel.gameObject.SetActive(true);
        ItemPanel.SetItemName(message);
    }

    public void HideItemNotification()
    {
        ItemPanel.gameObject.SetActive(false);
    }

    public void SendNotification(string message, Sprite sprite, Color color)
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
