using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionPanelController : MonoBehaviour
{
    public TMP_Text ItemNameText;
    public TMP_Text ItemDescriptionText;

    public void SetName(string name)
    {
        ItemNameText.text = name;
    }

    public void SetDescription(string description)
    {
        ItemDescriptionText.text = description;
    }
}
