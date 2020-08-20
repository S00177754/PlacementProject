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
    public LevelExpScriptableObj LevelGuide;
    public BackgroundMusicManager MusicManager;

    [Header("Stats")]
    public int Health = 100;
    public int MP = 60;
    public int MaxHealth = 100;
    public int MaxMP = 60;

    public const int StartingMaxHealth = 100;
    public const int StartingMaxMP = 60;

    public int Money = 0;

    public bool IsInvincible = false;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        Instance = this;
        CalculateMaxHP();
        CalculateMaxMP(); 
    }
    private void Start()
    {
        if(BackgroundMusicManager.Instance != null)
        BackgroundMusicManager.Instance.PlayTrack("Chill");

        if(MoneyHUDController.Instance != null)
        MoneyHUDController.Instance.SetAmount(Money);
    }

    public void ApplyDamage(int amount)
    {
        if (Health > 0)
        {
            Health -= (amount - (int)Math.Round((double) (GameStats.DefenceStat - 5) / 5) ); //Removes Damage being applied based on defense

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
        SceneManagerHelper.TransitionToScene(2);
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

    public void CalculateMaxHP()
    {
        MaxHealth = StartingMaxHealth + GetComponent<AbilityTreeManager>().GetHealthBonus() + ((GameStats.VitalityStat - 5) * 5) + GetComponent<EquipmentManager>().Loadout.GetHealthBonus();

        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }

    }

    public void CalculateMaxMP()
    {
        MaxMP = StartingMaxMP + GetComponent<AbilityTreeManager>().GetMagicBonus() + ((GameStats.MagicStat - 5) * 5) + GetComponent<EquipmentManager>().Loadout.GetMPBonus();

        if (MP > MaxMP)
        {
            MP = MaxMP;
        }

    }

    public void AddExperience(int xp)
    {
        GameStats.Experience += xp;

        for (int i = 0; i < LevelGuide.LevellingInfo.Count; i++)
        {
            if(GameStats.Experience >= LevelGuide.LevellingInfo[i].ExpRequired)
            {

                if(GameStats.Level != LevelGuide.LevellingInfo[i].Level)
                {

                    GameStats.Level = LevelGuide.LevellingInfo[i].Level;
                    GameStats.AbilityPoints += LevelGuide.LevellingInfo[i].AbilityPoints;
                    Debug.Log(string.Concat("Level: ",GameStats.Level,"  XP: ",GameStats.Experience,"  AP: ",GameStats.AbilityPoints));
                    return;
                }

            }
        }
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
    public string Name = "Akira Kurusu";

    public int Level = 1;
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
