using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBarHUD : HUDValueSlider
{
    private void Update()
    {
        SliderBar.value = (float)PlayerController.Instance.MP / (float)PlayerController.Instance.MaxMP;
        ValueText.text = PlayerController.Instance.MP.ToString();
    }
}
