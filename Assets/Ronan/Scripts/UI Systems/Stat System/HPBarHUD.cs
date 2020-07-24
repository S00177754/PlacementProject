using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarHUD : HUDValueSlider
{
    private void Update()
    {
        SliderBar.value = (float)PlayerController.Instance.Health / (float)PlayerController.Instance.MaxHealth;
        ValueText.text = PlayerController.Instance.Health.ToString();
    }
}
