using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangedNodeType { HealthBoost, AttackBoost, ComboUnlock, DualBoost }

[CreateAssetMenu(fileName = "Ranged Node", menuName = "Ability Tree/Ranged Node")]
[Serializable]
public class RangedAbilityNode : AbilityTreeNode
{
    [Header("Melee Details")]
    public RangedNodeType NodeType;
    public int HealthBonus = 0;
    public int AttackBonus = 0;
}
