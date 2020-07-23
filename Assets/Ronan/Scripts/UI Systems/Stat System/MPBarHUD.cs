using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBarHUD : HUDValueSlider
{
    [Header("Player")]
    public PlayerController Player;

    private void Start()
    {
        ValueNameText.text = "MP";
    }

    private void Update()
    {
        SliderBar.value = (float)Player.MP / (float)Player.GameStats.MaxMP;
        ValueText.text = Player.MP.ToString();
    }
}
