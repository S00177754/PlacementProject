using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRadialMenuController : RadialMenuController
{
    public InventoryManager InventoryManager;

    public Dictionary<int,ItemObj> ItemsToPopulate = new Dictionary<int, ItemObj>();

    private void Start()
    {
        PopulateWheel();
    }

    public override void UseMenuAction()
    {
        (Sections[segmentNum -1] as RadialItemSection).UseItem(InventoryManager);
        base.UseMenuAction();
    }

    public void PopulateWheel()
    {
        //for (int i = 0; i < ItemsToPopulate.Count; i++)
        //{
        //    (Sections[i] as RadialItemSection).SetItem(ItemsToPopulate[i]);
        //}

        foreach (var item in ItemsToPopulate)
        {
            (Sections[item.Key - 1] as RadialItemSection).SetItem(item.Value);
        }
    }

}
