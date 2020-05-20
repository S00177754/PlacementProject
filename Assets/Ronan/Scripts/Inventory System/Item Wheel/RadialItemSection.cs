using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialItemSection : MonoBehaviour
{
    public Image ItemImage;
    private Image PanelBackground;
    private ItemObj Item;
    private Color defaultColor;

    private void Start()
    {
        PanelBackground = GetComponent<Image>();
        defaultColor = PanelBackground.color;
    }

    public void SetItem(ItemObj obj) 
    {
        Item = obj;
        ItemImage.sprite = obj.ItemIcon;
    }

    public void HighlightSection(Color highlightColor)
    {
        PanelBackground.color = highlightColor;
    }

    public void ResetHighlight()
    {
        if(PanelBackground != null)
        PanelBackground.color = defaultColor;
    }
}
