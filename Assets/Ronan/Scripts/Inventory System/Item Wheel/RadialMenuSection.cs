using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuSection : MonoBehaviour
{
    public Image IconImage;
    private Image PanelBackground;
    private Color defaultColor;

    private void Start()
    {
        PanelBackground = GetComponent<Image>();
        defaultColor = PanelBackground.color;
    }

    public void HighlightSection(Color highlightColor)
    {
        PanelBackground.color = highlightColor;
    }

    public void ResetHighlight()
    {
        if (PanelBackground != null)
            PanelBackground.color = defaultColor;
    }
}
