using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTreeNode : ScriptableObject
{
    [Header("Nodes")]
    public List<AbilityTreeNode> PreRequirements;
    public List<AbilityTreeNode> NextNodes;

    [Header("Node Details")]
    public string IdName;
    public bool NodeUnlocked = false;
    public int AbilityPointCost;
}

public class AbilityNodeData
{
    public string IdName;
    public bool Unlocked;
}


