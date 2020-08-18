using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject 
{
    //ID used for Structured naming and easier comparison, Name is public view
    //EG ID: MSQ1, Name: Beginnings
    public string ID;
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

    public void GoToNextStep()
    {
        if(ActiveStep.isComplete)
        {
            CompletedSteps.Enqueue(ActiveStep);
            ActiveStep = Steps.Dequeue();
            NextStep = Steps.Peek();
        }
        AssignActiveStep();
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
        CheckCompletedSteps();
        AssignActiveStep();
    }

    void AssignActiveStep()
    {
        foreach (QuestStep step in StepsList)
        {
            if(!step.isComplete)
            {
                ActiveStep = step;
                break;
            }
        }
    }


    public void SwitchIsActive()
    {
        isActive = !isActive;
    }


    public void CheckCompletedSteps()
    {
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
