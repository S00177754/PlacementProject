using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRadialMenuController : RadialMenuController
{
    public override void UseMenuAction()
    {
        Debug.Log(string.Concat("Use Item: ", segmentNum));
        base.UseMenuAction();
    }

}
