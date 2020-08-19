using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillPointManipulator;

public class SkillPointPanelController : MonoBehaviour
{
    public List<SkillPointManipulator> SkillManipulators;
    public TMP_Text RemainingText;
    public TMP_Text NameText;
    public TMP_Text StatText;
    public TMP_Text StatNumbersText;

    public void Setup()
    {
        CharacterStats Stats = PlayerController.Instance.GameStats;
        RemainingText.text = string.Concat("Remaining Skill Points: ", Stats.SkillPoints);
        StatText.text = string.Concat("Level: ",Stats.Level,"\nEXP: ",Stats.Experience,"\n\nAbility Points: ",Stats.AbilityPoints);
        NameText.text = PlayerController.Instance.GameStats.Name;

        SkillManipulators.ForEach(sm => sm.PointsToAdd = 0);
        SkillManipulators.ForEach(sm => sm.UpdatePointValue());
        InteractableCheck();
    }

    public void InteractableCheck()
    {
        for (int i = 0; i < SkillManipulators.Count; i++)
        {
            if(SkillManipulators[i].PointsToAdd <= 0)
            {
                SkillManipulators[i].Subtract.interactable = false;
            }
            else
            {
                SkillManipulators[i].Subtract.interactable = true;
            }


            if (PlayerController.Instance.GameStats.SkillPoints <= GetPointTotal())
            {
                SkillManipulators[i].Addition.interactable = false;
            }
            else
            {
                SkillManipulators[i].Addition.interactable = true;
            }

        }

        CharacterStats Stats = PlayerController.Instance.GameStats;
        RemainingText.text = string.Concat("Remaining Skill Points: ", Stats.SkillPoints - GetPointTotal());
        StatNumbersText.text = string.Concat(Stats.StrengthStat,"\n",Stats.DexterityStat,"\n",Stats.VitalityStat,"\n",Stats.MagicStat,"\n",Stats.DefenceStat);
    }

    public int GetPointTotal()
    {
        int total = 0;
        SkillManipulators.ForEach(sm => total += sm.PointsToAdd);
        return total;
    }

    public void ConfirmPointAllocation()
    {

        foreach (var manipulator in SkillManipulators)
        {
            switch (manipulator.Type)
            {
                case SkillType.Strength:
                    PlayerController.Instance.GameStats.StrengthStat += manipulator.PointsToAdd;
                    break;

                case SkillType.Dexterity:
                    PlayerController.Instance.GameStats.DexterityStat += manipulator.PointsToAdd;
                    break;

                case SkillType.Vitality:
                    PlayerController.Instance.GameStats.VitalityStat += manipulator.PointsToAdd;
                    break;

                case SkillType.Magic:
                    PlayerController.Instance.GameStats.MagicStat += manipulator.PointsToAdd;
                    break;

                case SkillType.Defence:
                    PlayerController.Instance.GameStats.DefenceStat += manipulator.PointsToAdd;
                    break;

                default:
                    break;
            }

            PlayerController.Instance.GameStats.SkillPoints -= manipulator.PointsToAdd;
        }

        SkillManipulators.ForEach(sm => sm.PointsToAdd = 0);
        SkillManipulators.ForEach(sm => sm.UpdatePointValue());
        InteractableCheck();
        CharacterStats Stats = PlayerController.Instance.GameStats;
        RemainingText.text = string.Concat("Remaining Skill Points: ", Stats.SkillPoints);
        StatNumbersText.text = string.Concat(Stats.StrengthStat, "\n", Stats.DexterityStat, "\n", Stats.VitalityStat, "\n", Stats.MagicStat, "\n", Stats.DefenceStat);


    }
}
