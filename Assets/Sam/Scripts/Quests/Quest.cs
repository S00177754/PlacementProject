﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject 
{
    public int ID;
    public string Name;
    [TextArea(10, 15)]
    public string Description;
    public bool isActive;
    public bool isComplete;

    public NPC QuestGiver;
    public NPC QuestReturn;
    public Queue<QuestStep> CompletedSteps;
    public List<QuestStep> CompletedList;
    public Queue<QuestStep> Steps;
    public List<QuestStep> StepsList;
    public QuestStep ActiveStep;
    public QuestStep NextStep;

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
        foreach (QuestStep step in StepsList)
        {
            Steps.Enqueue(step);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Working on generic method to searchfor and add each step to the quest
    void GetQuests()
    {
        GameObject tryThis;
        QuestStep addThis;
        for (int i = 1; i < 50; i++)
        {
            tryThis = Resources.Load("Sam/QuestSystem/MSQ1S" + i.ToString()) as GameObject;
            //TryGetComponent<QuestStep>(Resources.Load("Sam/QuestSystem/MSQ1S" + i.ToString()), out addThis);
        }
    }
}
