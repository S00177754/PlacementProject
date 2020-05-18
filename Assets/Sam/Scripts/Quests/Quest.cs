using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
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
