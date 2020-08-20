using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestStep : ScriptableObject 
{    
    public string ID;
    //[SerializeField]
    //[TextArea(10, 15)]
    //public readonly string Description;
    //[SerializeField]
    //public readonly string Name; 
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