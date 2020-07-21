using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRadialMenuController : RadialMenuController
{
    //*************** Public Variables ********************
    [Header("Radial Elements")]
    public ItemWheelPanel InfoPanel;//Refactor to item because not all radial menus need a centre panel
    public InventoryManager InventoryManager;
    public Dictionary<int,ItemObj> ItemsToPopulate = new Dictionary<int, ItemObj>();

    //**************** Monobehaviour Methods ******************
    public override void Update()
    {
        base.Update();
    }

    //**************** Radial Input & Calculation ********************
    public override void Input(Vector2 input)
    {
        base.Input(input);
    }

    //**************** Functionality Methods ****************
    public override void UseSectionAction()
    {
        (Sections[segmentNum -1] as RadialItemSection).UseItem(InventoryManager);
        base.UseSectionAction();
    }

    public void PopulateWheel()
    {
        foreach (var item in ItemsToPopulate)
        {
            (Sections[item.Key - 1] as RadialItemSection).SetItem(item.Value,InventoryManager);
            (Sections[item.Key - 1] as RadialItemSection).ItemRadialMenu = this;
        }
    }

    //**************** Initialisation & Close Methods *********************
    public override void Startup()
    {
        base.Startup();

        InfoPanel.ItemNameField.text = "";
        InfoPanel.ItemDescriptionField.text = "";
        InfoPanel.ItemAmountField.text = "";

        PopulateWheel();
    }

    public override void CloseMenu()
    {
        base.CloseMenu();
    }

}
