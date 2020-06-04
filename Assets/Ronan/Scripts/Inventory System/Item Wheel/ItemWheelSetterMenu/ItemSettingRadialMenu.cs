using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSettingRadialMenu : RadialMenuController
{
    //*************** Public Variables ********************
    public ItemObj ItemToSet;
    public ItemRadialMenuController ItemRadialMenu;
    public InventoryItemUsagePanel ActivatedBy;

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
        if (ItemRadialMenu.ItemsToPopulate.ContainsKey(segmentNum))
        {
            ItemRadialMenu.ItemsToPopulate[segmentNum] = ItemToSet;
        }
        else
        {
            ItemRadialMenu.ItemsToPopulate.Add(segmentNum, ItemToSet);
        }
        ItemToSet = null;
    }

    public void SetInventoryItemUsagePanel(InventoryItemUsagePanel itemUsagePanel)
    {
        ActivatedBy = itemUsagePanel;
    }

    private void PopulateWheel()
    {
        foreach (var item in ItemRadialMenu.ItemsToPopulate)
        {
            (Sections[item.Key - 1] as ItemSetterRadialSection).SetupRadialSection(item.Value);
        }
    }

    //**************** Initialisation & Close Methods *********************
    public override void Startup()
    {
        base.Startup();

        PopulateWheel();
    }

    public override void CloseMenu()
    {
        base.CloseMenu();
    }


   

 
}
