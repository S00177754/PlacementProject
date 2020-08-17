using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum MerchantButtonMode { Buy, Sell }

public class MerchantListButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public MerchantButtonMode Mode;
    public ItemObj Item;

    [Header("Text Components")]
    public TMP_Text Txt_ItemName;
    public TMP_Text Txt_ItemCost;

    [Header("External Systems")]
    public MerchantUIController Merchant;

    public void Initialize(ItemObj item,MerchantButtonMode mode)
    {
        Item = item;
        Mode = mode;
        Setup();
    }

    private void Setup()
    {
        Merchant = MerchantUIController.Instance;
        Txt_ItemName.text = Item.Name;

        switch (Mode)
        {
            case MerchantButtonMode.Buy:
                Txt_ItemCost.text = Item.BuyPrice.ToString();
                break;

            case MerchantButtonMode.Sell:
                Txt_ItemCost.text = Item.SellPrice.ToString();
                break;
        }
    }

    public void ButtonAction()
    {
        switch (Mode)
        {
            case MerchantButtonMode.Buy:
                Buy();
                break;

            case MerchantButtonMode.Sell:
                Sell();
                break;

            default:
                Debug.LogError("Button Action Mode not set.");
                break;
        }
    }

    public void Buy()
    {
        if (PlayerController.Instance.TrySpendMoney(Item.BuyPrice))
        {
            PlayerController.Instance.GetComponent<InventoryManager>().Inventory.AddItem(Item, 1);
            Merchant.UpdateMoney();
            Merchant.GenerateList(MerchantButtonMode.Buy);
            MoneyHUDController.Instance.SetAmount(PlayerController.Instance.Money);
        }
    }

    public void Sell()
    {
        PlayerController.Instance.Money += Item.SellPrice;
        Merchant.UpdateMoney();
        MoneyHUDController.Instance.SetAmount(PlayerController.Instance.Money);
        PlayerController.Instance.GetComponent<InventoryManager>().Inventory.RemoveItem(Item, 1);
        
        if(PlayerController.Instance.GetComponent<InventoryManager>().Inventory.GetItemRemainingAmount(Item) <= 0)
        Merchant.GenerateList(MerchantButtonMode.Sell);
    }


    public void OnSelect(BaseEventData eventData)
    {
        Merchant.DescriptionPanel.SetName(Item.Name);
        Merchant.DescriptionPanel.SetDescription(Item.Description);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Merchant.DescriptionPanel.SetName(Item.Name);
        Merchant.DescriptionPanel.SetDescription(Item.Description);
    }
}
