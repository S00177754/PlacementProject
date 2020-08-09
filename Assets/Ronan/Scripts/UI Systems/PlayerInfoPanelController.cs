using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoPanelController : MonoBehaviour
{
    public TMP_Text PlayerName;
    public TMP_Text PlayerDetails;

    public void Setup()
    {
        CharacterStats Stats = PlayerController.Instance.GameStats;

        PlayerName.text = Stats.Name;

        PlayerDetails.text = string.Concat("Level: ", Stats.Level, "  EXP: ", Stats.Experience,"\nSkill Points: ",Stats.SkillPoints, "\nAbility Points: ", Stats.AbilityPoints,"\nMoney: ",PlayerController.Instance.Money);
        PlayerDetails.text = string.Concat(PlayerDetails.text, "\n\nStrength: ", Stats.StrengthStat,
                                                                "\nDexterity: ", Stats.DexterityStat,
                                                                "\nMagic: ", Stats.MagicStat,
                                                                "\nVitality: ", Stats.VitalityStat,
                                                                "\nDefence: ", Stats.DefenceStat);
    }
}
