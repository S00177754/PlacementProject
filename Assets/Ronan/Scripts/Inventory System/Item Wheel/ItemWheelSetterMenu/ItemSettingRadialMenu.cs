using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSettingRadialMenu : RadialMenuController
{
    public ItemObj ItemToSet;
    public ItemRadialMenuController ItemRadialMenu;
    public InventoryItemUsagePanel ActivatedBy;

    public override void Update()
    {
        base.Update();
    }

    public override void CloseMenu()
    {
        base.CloseMenu();
    }

    public override void SetInputAxis(Vector2 input)
    {
        base.SetInputAxis(input);
    }

    public override void Startup()
    {
        base.Startup();
    }

    public void Activate(InventoryItemUsagePanel itemUsagePanel)
    {
        ActivatedBy = itemUsagePanel;
    }

    public void PopulateWheel()
    {
        foreach (var item in ItemRadialMenu.ItemsToPopulate)
        {
            (Sections[item.Key - 1] as ItemSetterRadialSection).SetupRadialSection(item.Value);
        }
    }

    public override void UseMenuAction()
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
        base.UseMenuAction();
    }

 
}
