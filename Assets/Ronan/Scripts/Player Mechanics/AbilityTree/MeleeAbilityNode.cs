using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeNodeType { HealthBoost, AttackBoost, ComboUnlock, DualBoost}

[CreateAssetMenu(fileName = "Melee Node", menuName = "Ability Tree/Melee Node")]
[Serializable]
public class MeleeAbilityNode : AbilityTreeNode
{
    [Header("Melee Details")]
    public MeleeNodeType NodeType;
    public int HealthBonus = 0;
    public int AttackBonus = 0;
}
