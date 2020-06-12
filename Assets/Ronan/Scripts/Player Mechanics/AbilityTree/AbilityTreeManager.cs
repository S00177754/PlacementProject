using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeManager : MonoBehaviour
{
    public MeleeAbilityTree MeleeTree;
    public MagicAbilityTree MagicTree;

    public int GetHealthBonus()
    {
        int hpBonus = MeleeTree.GetHealthBoost();
        hpBonus += MagicTree.GetHealthBoost();

        return hpBonus;
    }

    public int GetAttackBonus()
    {
        return MeleeTree.GetAttackBoost();
    }

    public float GetMagicBonus()
    {
        return MagicTree.GetMagicBoost();
    }
}
