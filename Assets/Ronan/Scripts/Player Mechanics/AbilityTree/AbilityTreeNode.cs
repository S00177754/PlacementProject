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

    public virtual string GetDescription()
    {
        string description = "";

        return description;
    }

    public bool PreviousNodesUnlocked()
    {
        foreach (var req in PreRequirements)
        {
            if (!req.NodeUnlocked)
            {
                return false;
            }
        }

        return true;
    }
}




