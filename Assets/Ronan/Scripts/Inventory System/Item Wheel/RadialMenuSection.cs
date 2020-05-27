using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuSection : MonoBehaviour
{
    protected RadialMenuController Controller;
    public Image IconImage;
    protected Image PanelBackground;
    protected Color defaultColor;

    public virtual void Start()
    {
        SetupComponents();
    }

    public virtual void SetupComponents()
    {
        Controller = GetComponentInParent<RadialMenuController>();
        PanelBackground = GetComponent<Image>();
        defaultColor = PanelBackground.color;
    }

    public virtual void HighlightSection(Color highlightColor)
    {
        PanelBackground.color = highlightColor;
    }

    public void ResetHighlight()
    {
        if (PanelBackground != null)
            PanelBackground.color = defaultColor;
    }
}
