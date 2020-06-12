using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MeleeAbilityTree
{
    public MeleeAbilityNode RootNode;

    public int GetHealthBoost()
    {
        return CalculateHealthBonus(RootNode);
    }

    private int CalculateHealthBonus(MeleeAbilityNode node)
    {
        int healthBonus = 0;
        
        if(node.NodeUnlocked)
        healthBonus += node.HealthBonus;

        foreach (MeleeAbilityNode child in node.NextNodes)
        {
            healthBonus += CalculateHealthBonus(child);
        }

        return healthBonus;
    }


    public int GetAttackBoost()
    {
        return CalculateAttackBonus(RootNode);
    }

    private int CalculateAttackBonus(MeleeAbilityNode node)
    {
        int attackBonus = 0;

        if (node.NodeUnlocked)
            attackBonus += node.AttackBonus;

        foreach (MeleeAbilityNode child in node.NextNodes)
        {
            attackBonus += CalculateAttackBonus(child);
        }

        return attackBonus;
    }


    public int GetUnlockedCombos()
    {
        return CalculateComboCount(RootNode);
    }

    private int CalculateComboCount(MeleeAbilityNode node)
    {
        int comboCount = 0;

        if(node.NodeType == MeleeNodeType.ComboUnlock && node.NodeUnlocked)
        {
            comboCount = 1;
        }

        foreach (MeleeAbilityNode child in node.NextNodes)
        {
            comboCount += CalculateComboCount(child);
        }

        return comboCount;
    }
}

[Serializable]
public class MagicAbilityTree
{
    public MagicAbilityNode RootNode;

    public int GetHealthBoost()
    {
        return CalculateHealthBonus(RootNode);
    }

    private int CalculateHealthBonus(MagicAbilityNode node)
    {
        int healthBonus = 0;

        if (node.NodeUnlocked)
            healthBonus += node.HealthBonus;

        foreach (MagicAbilityNode child in node.NextNodes)
        {
            healthBonus += CalculateHealthBonus(child);
        }

        return healthBonus;
    }

    public float GetMagicBoost()
    {
        return CalculateMagicBonus(RootNode);
    }

    private float CalculateMagicBonus(MagicAbilityNode node)
    {
        float magicBonus = 0;

        if (node.NodeUnlocked)
            magicBonus += node.MagicBonus;

        foreach (MagicAbilityNode child in node.NextNodes)
        {
            magicBonus += CalculateMagicBonus(child);
        }

        return magicBonus;
    }
}
