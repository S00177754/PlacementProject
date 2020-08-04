using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;
    public CharacterStats GameStats;
    public PlayerSettings Settings;

    [Header("External References")]
    public PlayerHUDController HUDController;
    public PauseMenuController PauseMenu;

    [Header("Stats")]
    public int Health = 100;
    public int MP = 60;
    public int MaxHealth = 100;
    public int MaxMP = 60;

    public int Money = 0;

    public bool IsInvincible = false;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        Instance = this;
    }

    public void ApplyDamage(int amount)
    {
        if (Health > 0)
        {
            Health -= amount;

            if (Health <= 0)
            {
                StartCoroutine(DeathLogic());
            }
        }
    }

    private IEnumerator DeathLogic()
    {
        //TODO Death animation
        Debug.Log("Oh damn im deaded");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

    public bool TrySpendMoney(int amount)
    {
        if(Money >= amount)
        {
            Money -= amount;
            return true;
        }

        return false;
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
    public static float EnemyDespawnRange = 150f;

    [Header("Camera Settings")]
    public bool InvertYAxis = true;
    public float CameraSensitivity = 2f;
}

public enum PlayerStatTypes { Strength, Dexterity, Magic, Vitality, Defence}

[Serializable]
public class CharacterStats
{
    public string Name;

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
