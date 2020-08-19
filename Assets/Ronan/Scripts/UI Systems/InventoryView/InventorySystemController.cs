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
    public GameObject BaubleMenu;
    public RectTransform ListContent;
    public DescriptionPanelController DescriptionPanel;

    [Header("Filter Elements")]
    public TMP_Text ItemFilterHeader;
    public List<Image> FilterDots;
    public Sprite ActiveFilterDot;
    public Sprite InactiveFilterDot;

    public Button PreviousFilterButton;
    public Button NextFilterButton;

    private ItemType ListFilter = ItemType.Potion;
    private int childAmount = 0;


    public void Initialize()
    {
        ListFilter = ItemType.Potion;
        FilterList();
        BaubleMenu.SetActive(false);
    }

    public void ClearList()
    {
        ItemListActive = false;
        for (int i = 0; i < ListContent.childCount; i++)
        {
            Destroy(ListContent.GetChild(i).gameObject);
            childAmount--;
        }
        ItemUsageMenu.SetActive(false);
    }

    public void FilterList()
    {
        childAmount = ListContent.childCount;
        ClearList();

        for (int i = 0; i < TargetInventory.Inventory.Collection.Count; i++)
        {
            if (TargetInventory.Inventory.Collection[i].Item.Type == ListFilter)
            {
                GameObject panel = Instantiate(ItemButtonPrefab, ListContent);
                panel.GetComponent<InventoryItemPanel>().SetDetails(TargetInventory.Inventory.Collection[i], this);
                childAmount++;
            }
        }

        

        if (childAmount > 0)
        {
            for (int i = 0; i < childAmount; i++)
            {
                Button btn = ListContent.GetChild(i).GetComponent<Button>();

                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.Explicit;
                nav.selectOnLeft = PreviousFilterButton;
                nav.selectOnRight = NextFilterButton;

                if (i - 1 >= 0)
                    nav.selectOnUp = ListContent.GetChild(i - 1).GetComponent<Button>();

                if(i + 1 < ListContent.childCount)
                    nav.selectOnDown = ListContent.GetChild(i + 1).GetComponent<Button>();

                btn.navigation = nav;
            }

            if (childAmount > 1)
            {
                Navigation navTop = new Navigation();
                navTop.mode = Navigation.Mode.Explicit;
                navTop.selectOnDown = ListContent.GetChild(1).gameObject.GetComponent<Button>();
                navTop.selectOnLeft = PreviousFilterButton;
                navTop.selectOnRight = NextFilterButton;
                navTop.selectOnUp = NextFilterButton;
                ListContent.GetChild(0).gameObject.GetComponent<Button>().navigation = navTop;

                Navigation navBottom = new Navigation();
                navBottom.mode = Navigation.Mode.Explicit;
                navBottom.selectOnUp = ListContent.GetChild(childAmount - 2).gameObject.GetComponent<Button>();
                navBottom.selectOnLeft = PreviousFilterButton;
                navBottom.selectOnRight = NextFilterButton;
                ListContent.GetChild(childAmount - 1).gameObject.GetComponent<Button>().navigation = navBottom;

                Navigation navLeft = PreviousFilterButton.navigation;
                navLeft.selectOnDown = ListContent.GetChild(0).gameObject.GetComponent<Button>();
                PreviousFilterButton.navigation = navLeft;

                Navigation navRight = NextFilterButton.navigation;
                navRight.selectOnDown = ListContent.GetChild(0).gameObject.GetComponent<Button>();
                NextFilterButton.navigation = navRight;
            
            
            }

            UIHelper.SelectedObjectSet(ListContent.GetChild(0).gameObject);
        }
        else
        {
            UIHelper.SelectedObjectSet(NextFilterButton.gameObject);
        }


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
