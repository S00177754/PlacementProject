using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSetterRadialSection : RadialMenuSection
{
    //*************** Public Variables ********************
    public TMP_Text ItemNameText;

    //**************** Monobehaviour Methods ******************
    public override void Start()
    {
        SetIconAlpha(0);
        base.Start();

    }

    //**************** Initialisation & Close Methods *********************
    public override void SetupComponents()
    {
        base.SetupComponents();
    }

    //**************** Graphical Methods *********************
    public override void HighlightSection(Color unrestricted, Color restricted)
    {
        base.HighlightSection(unrestricted, restricted);
    }



    //**************** Functionality Methods ****************
    public void SetupRadialSection(ItemObj item)
    {
        if(item == null)
        {
            ItemNameText.text = "";
            SetIconAlpha(0);
            return;
        }

        SetIconAlpha(1);
        ItemNameText.text = item.Name;
        IconImage.sprite = item.ItemIcon;
    }

    

    
}
