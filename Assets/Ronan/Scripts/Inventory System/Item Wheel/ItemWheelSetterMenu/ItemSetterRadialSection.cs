using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSetterRadialSection : RadialMenuSection
{
    public TMP_Text ItemNameText;

    public void SetupRadialSection(ItemObj item)
    {
        if(item == null)
        {
            ItemNameText.text = "";
            
            return;
        }

        ItemNameText.text = item.Name;
        IconImage.sprite = item.ItemIcon;
    }

    public override void HighlightSection(Color highlightColor)
    {
        base.HighlightSection(highlightColor);
    }

    public override void Start()
    {
        base.Start();
    }
}
