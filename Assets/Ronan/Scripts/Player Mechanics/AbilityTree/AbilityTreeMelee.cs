using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeMelee : MonoBehaviour
{
    //public List<MeleeAbilityTreeNode> MeleeNodes;
    public MeleeAbilityTreeNode RootNode;

    //**************** Info Methods ********************
    public int GetHPBonus(MeleeAbilityTreeNode node)
    {
        if (node.NodeUnlocked && node.NodeType == MeleeNodeType.Health)
        {
            int hpBonus = node.HealthBonus;

            foreach (var nextNode in node.NextNodes)
            {
                hpBonus += GetHPBonus(nextNode as MeleeAbilityTreeNode);
            }

            return hpBonus;
        }
        else return 0;
    }

    public int GetUnlockedComboCount(MeleeAbilityTreeNode node)
    {
        if (node.NodeUnlocked && node.NodeType == MeleeNodeType.Combo)
        {
            int comboCount = 0;
                comboCount++;

            foreach (var nextNode in node.NextNodes)
            {
                comboCount += GetUnlockedComboCount(nextNode as MeleeAbilityTreeNode);
            }

            return comboCount;
        }
        else return 0;
    }

    private float GetAttackBonus(MeleeAbilityTreeNode node)
    {
        if (node.NodeUnlocked && node.NodeType == MeleeNodeType.Attack)
        {
            float attackBonus = node.AttackBonus;

            foreach (var nextNode in node.NextNodes)
            {
                attackBonus += GetAttackBonus(nextNode as MeleeAbilityTreeNode);
            }

            return attackBonus;
        }
        else return 0;
    }
}
