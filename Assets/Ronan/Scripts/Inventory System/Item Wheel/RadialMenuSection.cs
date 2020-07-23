using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuSection : MonoBehaviour
{
    //*************** Public Variables ********************
    public Image IconImage;

    //*************** Private Variables ******************
    protected Image PanelBackground;
    protected Color defaultColor;
    protected RadialMenuController Controller;

    //**************** Monobehaviour Methods ******************
    public virtual void Start()
    {
        SetupComponents();
    }

    //**************** Initialisation & Close Methods *********************
    public virtual void SetupComponents()
    {
        Controller = GetComponentInParent<RadialMenuController>();
        PanelBackground = GetComponent<Image>();
        defaultColor = PanelBackground.color;
    }

    //**************** Graphical Methods *********************
    public virtual void HighlightSection(Color unrestricted,Color restricted)
    {
        PanelBackground.color = unrestricted;
    }

    public void ResetHighlight()
    {
        if (PanelBackground != null)
            PanelBackground.color = defaultColor;
    }

    protected void SetIconAlpha(float alpha)
    {
        Color color = IconImage.color;
        color.a = alpha;
        IconImage.color = color;
    }
}
