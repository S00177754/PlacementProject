using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeNodeType { Attack, Health, Combo }

[CreateAssetMenu(fileName = "New Melee Node Object", menuName = "AbilityTree/Melee Node")]
public class MeleeAbilityTreeNode : AbilityTreeNode
{
    [Header("Melee Node Details")]
    public MeleeNodeType NodeType;
    public float AttackBonus;
    public int HealthBonus;
}
