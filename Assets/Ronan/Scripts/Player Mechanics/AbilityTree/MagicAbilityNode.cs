using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicNodeType { HealthBoost, MagicBoost, ComboUnlock, DualBoost }

[CreateAssetMenu(fileName = "Magic Node", menuName = "Ability Tree/Magic Node")]
[Serializable]
public class MagicAbilityNode : AbilityTreeNode
{
    [Header("Magic Details")]
    public MagicNodeType NodeType;
    public int HealthBonus = 0;
    public int MagicBonus = 0;
}
