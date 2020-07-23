using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStep", menuName = "Quest System/QuestStep", order = 0)]
public class QuestStep : ScriptableObject {
    
    protected int ID;
    //[SerializeField]
    //[TextArea(10, 15)]
    //public readonly string Description;
    //[SerializeField]
    //public readonly string Name; 
    public bool isComplete;

    public virtual string GetName()
    {
        return string.Empty;
    }

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}