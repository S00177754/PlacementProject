using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject 
{
    public int ID;
    public string Name;
    public string Description;
    NPC QuestGiver;
    NPC QuestReturn;
    bool isActive;
    bool isComplete;
    Queue<QuestStep> Steps;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
