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
        if (MeleeTree.RootNode != null)
        {
            return MeleeTree.GetUnlockedCombos();
        }
        return 1;
    }

    public int GetRangedComboLimit()
    {
        if(RangedTree.RootNode != null)
        {
            return RangedTree.GetUnlockedCombos();
        }
        return 1;
    }

    public int GetMagicBonus()
    {
        return MagicTree.GetMagicBoost();
    }
}
