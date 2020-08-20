using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestStep : ScriptableObject 
{    
    public string ID;
    public bool isComplete;
    public Quest ParentQuest;

    public virtual string GetName()
    {
        return string.Empty;
    }

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}