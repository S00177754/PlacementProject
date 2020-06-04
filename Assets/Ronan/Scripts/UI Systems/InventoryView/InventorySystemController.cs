using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystemController : MonoBehaviour
{
    public bool ItemListActive = false;

    [Header("External Requirements")]
    public InventoryManager TargetInventory;

    [Header("Inventory System Components")]
    public GameObject InventoryList;
    public GameObject RadialMenu;
    public GameObject ItemButtonPrefab;
    public GameObject ItemUsageMenu;
    public RectTransform ListContent;
    public DescriptionPanelController DescriptionPanel;

    [Header("Filter Elements")]
    public TMP_Text ItemFilterHeader;
    public List<Image> FilterDots;
    public Sprite ActiveFilterDot;
    public Sprite InactiveFilterDot;

    private ItemType ListFilter = ItemType.Potion;



    public void Initialize()
    {
        ListFilter = ItemType.Potion;
        FilterList();
    }

    public void ClearList()
    {
        ItemListActive = false;
        for (int i = 0; i < ListContent.childCount; i++)
        {
            Destroy(ListContent.GetChild(i).gameObject);
        }
        ItemUsageMenu.SetActive(false);
    }

    public void FilterList()
    {
        ClearList();

        for (int i = 0; i < TargetInventory.Inventory.Collection.Count; i++)
        {
            if (TargetInventory.Inventory.Collection[i].Item.Type == ListFilter)
            {
                GameObject panel = Instantiate(ItemButtonPrefab, ListContent);
                panel.GetComponent<InventoryItemPanel>().SetDetails(TargetInventory.Inventory.Collection[i], this);
            }
        }

        if (ListContent.childCount > 0)
            UIHelper.SelectedObjectSet(ListContent.GetChild(0).gameObject);
        ItemListActive = true;

        UpdateFilterDots();
    }

    private void UpdateFilterDots()
    {
        for (int i = 0; i < Enum.GetNames(typeof(ItemType)).Length; i++)
        {
            if (i != (int)ListFilter)
            {
                FilterDots[i].sprite = InactiveFilterDot;
            }
            else
            {
                FilterDots[i].sprite = ActiveFilterDot;
                ItemFilterHeader.text = Enum.GetNames(typeof(ItemType))[i];
            }
        }
    }


    public void NextFilter()
    {
        int nextFilter = (int)ListFilter + 1;

        if (nextFilter >= Enum.GetNames(typeof(ItemType)).Length)
            nextFilter = 0;

        ListFilter = (ItemType)nextFilter;

        FilterList();
    }

    public void PreviousFilter()
    {
        int nextFilter = (int)ListFilter - 1;

        if (nextFilter < 0)
            nextFilter = Enum.GetNames(typeof(ItemType)).Length - 1;

        ListFilter = (ItemType)nextFilter;

        FilterList();
    }


    public void ShowInventoryList()
    {
        InventoryList.SetActive(true);
        RadialMenu.SetActive(false);
    }

    public void ShowRadialMenu()
    {
        InventoryList.SetActive(false);
        RadialMenu.SetActive(true);
    }
}
