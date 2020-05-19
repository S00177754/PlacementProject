using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject 
{
    public int ID;
    public string Name;
    public string Description;
    public bool isActive;
    public bool isComplete;
    
    NPC QuestGiver;
    NPC QuestReturn;
    Queue<QuestStep> CompletedSteps;
    Queue<QuestStep> Steps;
    QuestStep ActiveStep;
    QuestStep NextStep;

    public void GoToNextStep(){
        if(ActiveStep.isComplete){
            CompletedSteps.Enqueue(ActiveStep);
            ActiveStep = Steps.Dequeue();
            NextStep = Steps.Peek();
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
