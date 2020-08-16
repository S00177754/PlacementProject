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

    public void Initialize(ItemObj item)
    {
        Item = item;
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

    }

    public void Sell()
    {

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
