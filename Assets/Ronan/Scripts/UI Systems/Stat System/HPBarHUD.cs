using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarHUD : HUDValueSlider
{
    [Header("Player")]
    public PlayerController Player;

    private void Start()
    {
        ValueNameText.text = "HP";
    }

    private void Update()
    {
        SliderBar.value = (float)Player.GameStats.Health / (float)Player.GameStats.MaxHealth;
        ValueText.text = Player.GameStats.Health.ToString();
    }
}
