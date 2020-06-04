using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeNode : ScriptableObject
{
    [Header("Nodes")]
    public List<AbilityTreeNode> PreRequirements;
    public List<AbilityTreeNode> NextNodes;

    [Header("Node Details")]
    public bool NodeUnlocked = false;
    public int AbilityPointCost;
}


