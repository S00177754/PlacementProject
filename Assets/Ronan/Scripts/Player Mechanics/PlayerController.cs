using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;

    public string PlayerName;
    public CharacterStats GameStats;
    public PlayerSettings Settings;
    public PlayerHUDController HUDController;
    public PauseMenuController PauseMenu;

    public int Health = 100;
    public int MP = 60;
    public int MaxHealth = 100;
    public int MaxMP = 60;

    public bool IsInvincible = false;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        Instance = this;
    }

    public void ApplyDamage(int amount)
    {
        Health -= amount;
    }



    private void CalculateMaxHP()
    {
        MaxHealth = 100 + GetComponent<AbilityTreeManager>().GetHealthBonus() + ((GameStats.VitalityStat - 5) * 5);
    }

    private void CalculateMaxMP()
    {
        MaxMP = 100 + GetComponent<AbilityTreeManager>().GetMagicBonus() + ((GameStats.MagicStat - 5) * 5);
    }
}

[Serializable]
public class PlayerSettings
{
    [Header("Camera Settings")]
    public bool InvertYAxis = true;
    public float CameraSensitivity = 2f;
}

public enum PlayerStatTypes { Strength, Dexterity, Magic, Vitality, Defence}

[Serializable]
public class CharacterStats
{
    public int Experience = 0;
    public int SkillPoints = 0;
    public int AbilityPoints = 0;

    [Header("Stats")]
    public int StrengthStat = 5;
    public int DexterityStat = 5;
    public int MagicStat = 5;
    public int VitalityStat = 5;
    public int DefenceStat = 5;

    

}
