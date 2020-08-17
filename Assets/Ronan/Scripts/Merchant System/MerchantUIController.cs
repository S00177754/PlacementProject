using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MerchantUIController : MonoBehaviour
{
    public static MerchantUIController Instance;

    public GameObject ButtonPrefab;
    public MerchantComponent ActiveMerchant;
    public bool MerchantActive = false;

    public SubMenu RootMenu;
    public SubMenu ItemMenu;
    public GameObject BackgroundImage;

    [Header("External Components")]
    public RectTransform ListContent;
    public DescriptionPanelController DescriptionPanel;
    public TMP_Text Txt_PlayerMoney;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
        RootMenu.SubMenuObject.SetActive(false);
        ItemMenu.SubMenuObject.SetActive(false);
        DescriptionPanel.gameObject.SetActive(false);
        BackgroundImage.SetActive(false);
    }

    public void SetMerchant(MerchantComponent merchant)
    {
        ActiveMerchant = merchant;
    }

    //Sub Menus
    public void ShowBuyMenu()
    {
        RootMenu.SubMenuObject.SetActive(false);
        ItemMenu.SubMenuObject.SetActive(true);
        DescriptionPanel.gameObject.SetActive(true);
        UpdateMoney();
        GenerateList(MerchantButtonMode.Buy);
        MerchantActive = true;
    }

    public void ShowSellMenu()
    {
        RootMenu.SubMenuObject.SetActive(false);
        ItemMenu.SubMenuObject.SetActive(true);
        DescriptionPanel.gameObject.SetActive(true);
        UpdateMoney();
        GenerateList(MerchantButtonMode.Sell);
        MerchantActive = true;
    }

    public void ShowRootMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        RootMenu.SubMenuObject.SetActive(true);
        ItemMenu.SubMenuObject.SetActive(false);
        DescriptionPanel.gameObject.SetActive(false);
        BackgroundImage.SetActive(true);
        MerchantActive = true;
    }

    public void ExitMerchant()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        RootMenu.SubMenuObject.SetActive(false);
        ItemMenu.SubMenuObject.SetActive(false);
        DescriptionPanel.gameObject.SetActive(false);
        BackgroundImage.SetActive(false);
        GameStateController.Instance.ChangeAllPlayerMapsTo("Player");
        MerchantActive = false;
    }

    public void UpdateMoney()
    {
        Txt_PlayerMoney.text = PlayerController.Instance.Money.ToString();
    }

    public bool IsMerchantActive()
    {
        return MerchantActive;
    }

    //List Generation
    private int childAmount = 0;

    public void ClearList()
    {
        for (int i = 0; i < ListContent.childCount; i++)
        {
            Destroy(ListContent.GetChild(i).gameObject);
            childAmount--;
        }
        
    }

    public void GenerateList(MerchantButtonMode mode)
    {
        ClearList();
        UpdateMoney();
        switch (mode)
        {

            case MerchantButtonMode.Sell:
                for (int i = 0; i < PlayerController.Instance.GetComponent<InventoryManager>().Inventory.Collection.Count; i++)
                {
                    if (PlayerController.Instance.GetComponent<InventoryManager>().Inventory.Collection[i].Item.Type == ItemType.Treasure && PlayerController.Instance.GetComponent<InventoryManager>().Inventory.Collection[i].Amount > 0)
                    {
                        GameObject btn = Instantiate(ButtonPrefab, ListContent);
                        btn.GetComponent<MerchantListButton>().Initialize(PlayerController.Instance.GetComponent<InventoryManager>().Inventory.Collection[i].Item, MerchantButtonMode.Sell);
                        childAmount++;
                    }
                }
                break;

            case MerchantButtonMode.Buy:
                for (int i = 0; i < ActiveMerchant.ItemsToSell.Count; i++)
                {
                    GameObject btn = Instantiate(ButtonPrefab, ListContent);
                    btn.GetComponent<MerchantListButton>().Initialize(ActiveMerchant.ItemsToSell[i],MerchantButtonMode.Buy);
                    childAmount++;              
                }
                break;

        }

        #region Navigation Code
        //if (childAmount > 0)
        //{
        //    for (int i = 0; i < childAmount; i++)
        //    {
        //        Button btn = ListContent.GetChild(i).GetComponent<Button>();

        //        Navigation nav = new Navigation();
        //        nav.mode = Navigation.Mode.Explicit;
        //        nav.selectOnLeft = PreviousFilterButton;
        //        nav.selectOnRight = NextFilterButton;

        //        if (i - 1 >= 0)
        //            nav.selectOnUp = ListContent.GetChild(i - 1).GetComponent<Button>();

        //        if (i + 1 < ListContent.childCount)
        //            nav.selectOnDown = ListContent.GetChild(i + 1).GetComponent<Button>();

        //        btn.navigation = nav;
        //    }

        //    if (childAmount > 1)
        //    {
        //        Navigation navTop = new Navigation();
        //        navTop.mode = Navigation.Mode.Explicit;
        //        navTop.selectOnDown = ListContent.GetChild(1).gameObject.GetComponent<Button>();
        //        navTop.selectOnLeft = PreviousFilterButton;
        //        navTop.selectOnRight = NextFilterButton;
        //        navTop.selectOnUp = NextFilterButton;
        //        ListContent.GetChild(0).gameObject.GetComponent<Button>().navigation = navTop;

        //        Navigation navBottom = new Navigation();
        //        navBottom.mode = Navigation.Mode.Explicit;
        //        navBottom.selectOnUp = ListContent.GetChild(childAmount - 2).gameObject.GetComponent<Button>();
        //        navBottom.selectOnLeft = PreviousFilterButton;
        //        navBottom.selectOnRight = NextFilterButton;
        //        ListContent.GetChild(childAmount - 1).gameObject.GetComponent<Button>().navigation = navBottom;

        //        Navigation navLeft = PreviousFilterButton.navigation;
        //        navLeft.selectOnDown = ListContent.GetChild(0).gameObject.GetComponent<Button>();
        //        PreviousFilterButton.navigation = navLeft;

        //        Navigation navRight = NextFilterButton.navigation;
        //        navRight.selectOnDown = ListContent.GetChild(0).gameObject.GetComponent<Button>();
        //        NextFilterButton.navigation = navRight;


        //    }

        //    UIHelper.SelectedObjectSet(ListContent.GetChild(0).gameObject);
        //}
        //else
        //{
        //    UIHelper.SelectedObjectSet(NextFilterButton.gameObject);
        //}
        #endregion
    }
}
