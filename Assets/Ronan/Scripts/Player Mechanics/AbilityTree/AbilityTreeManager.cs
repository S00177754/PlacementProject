using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeManager : MonoBehaviour
{
    public MeleeAbilityTree MeleeTree;
    public MagicAbilityTree MagicTree;
    public RangedAbilityTree RangedTree;

    public int GetHealthBonus()
    {
        int hpBonus = MeleeTree.GetHealthBoost();
        hpBonus += MagicTree.GetHealthBoost();
        hpBonus += RangedTree.GetHealthBoost();
        return hpBonus;
    }

    public int GetAttackBonus()
    {
        return MeleeTree.GetAttackBoost();
    }

    public int GetRangedBonus()
    {
        return RangedTree.GetAttackBoost();
    }

    public int GetMeleeComboLimit()
    {
        return MeleeTree.GetUnlockedCombos();
    }

    public int GetRangedComboLimit()
    {
        return RangedTree.GetUnlockedCombos();
    }

    public int GetMagicBonus()
    {
        return MagicTree.GetMagicBoost();
    }
}
