using System.Collections;
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
    [SerializeField]
    public bool isMainScenario;

    public NPCDialogueTrigger QuestGiver;
    public NPCDialogueTrigger QuestReturn;
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

    void Start()
    {
        foreach (QuestStep step in StepsList)
        {
            step.ParentQuest = this;
            Steps.Enqueue(step);
            if(ActiveStep == null && !step.isComplete)
                ActiveStep = step;

        }
    }

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

    public void CheckCompletedSteps()
    {
        CompletedList = new List<QuestStep>();
        CompletedSteps = new Queue<QuestStep>();
        foreach (QuestStep step in StepsList)
        {
            if (step.isComplete)
            {
                CompletedList.Add(step);
                CompletedSteps.Enqueue(step);
            }
        }
    }
}
