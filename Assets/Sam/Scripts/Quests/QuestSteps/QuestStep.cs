using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStep", menuName = "Quest System/QuestStep", order = 0)]
public class QuestStep : ScriptableObject {
    
    protected int ID;
    [SerializeField]
    [TextArea(10, 15)]
    public readonly string Description;
    public readonly string Name; //Will likely be coded for creating quests
    public bool isComplete;
}