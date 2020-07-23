using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBarHUD : HUDValueSlider
{
    [Header("Player")]
    public PlayerController Player;

    private void Update()
    {
        SliderBar.value = (float)Player.MP / (float)Player.MaxMP;
        ValueText.text = Player.MP.ToString();
    }
}
