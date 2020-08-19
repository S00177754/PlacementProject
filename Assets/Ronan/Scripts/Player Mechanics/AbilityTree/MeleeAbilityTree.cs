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

        if (node != null)
        {


            if (node.NodeUnlocked)
                healthBonus += node.HealthBonus;

            foreach (MeleeAbilityNode child in node.NextNodes)
            {
                healthBonus += CalculateHealthBonus(child);
            }

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
        if (node != null)
        {
            if (node.NodeUnlocked)
                attackBonus += node.AttackBonus;

            foreach (MeleeAbilityNode child in node.NextNodes)
            {
                attackBonus += CalculateAttackBonus(child);
            }
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

        if (node != null)
        {

            if (node.NodeType == MeleeNodeType.ComboUnlock && node.NodeUnlocked)
            {
                comboCount = 1;
            }

            foreach (MeleeAbilityNode child in node.NextNodes)
            {
                comboCount += CalculateComboCount(child);
            }
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

        if (node != null)
        {


            if (node.NodeUnlocked)
                healthBonus += node.HealthBonus;

            foreach (MagicAbilityNode child in node.NextNodes)
            {
                healthBonus += CalculateHealthBonus(child);
            }

        }

        return healthBonus;
    }

    public int GetMagicBoost()
    {
        return CalculateMagicBonus(RootNode);
    }

    private int CalculateMagicBonus(MagicAbilityNode node)
    {
        int magicBonus = 0;

        if (node != null)
        {

            if (node.NodeUnlocked)
                magicBonus += node.MagicBonus;

            foreach (MagicAbilityNode child in node.NextNodes)
            {
                magicBonus += CalculateMagicBonus(child);
            }
        }
        return magicBonus;
    }
}

[Serializable]
public class RangedAbilityTree
{
    public RangedAbilityNode RootNode;

    public int GetHealthBoost()
    {
        return CalculateHealthBonus(RootNode);
    }

    private int CalculateHealthBonus(RangedAbilityNode node)
    {
        int healthBonus = 0;
        if(node != null)
        {

            if (node.NodeUnlocked)
                healthBonus += node.HealthBonus;

            foreach (RangedAbilityNode child in node.NextNodes)
            {
                healthBonus += CalculateHealthBonus(child);
            }

        }
        return healthBonus;
    }

    public int GetAttackBoost()
    {
        return CalculateAttackBonus(RootNode);
    }

    private int CalculateAttackBonus(RangedAbilityNode node)
    {
        int attackBonus = 0;

        if (node != null)
        {

            if (node.NodeUnlocked)
                attackBonus += node.AttackBonus;

            foreach (RangedAbilityNode child in node.NextNodes)
            {
                attackBonus += CalculateAttackBonus(child);
            }
        }
        return attackBonus;
    }

    public int GetUnlockedCombos()
    {
        return CalculateComboCount(RootNode);
    }

    private int CalculateComboCount(RangedAbilityNode node)
    {
        int comboCount = 0;
        if (node != null)
        {
            if (node.NodeType == RangedNodeType.ComboUnlock && node.NodeUnlocked)
            {
                comboCount = 1;
            }

            foreach (RangedAbilityNode child in node.NextNodes)
            {
                comboCount += CalculateComboCount(child);
            }
        }
        return comboCount;
    }
}
