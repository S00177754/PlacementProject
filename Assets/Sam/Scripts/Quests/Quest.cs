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

    public bool isFound;

    [SerializeField]
    public bool isMainScenario;
    [SerializeField]
    QuestManager QuestManager;

    public NPCDialogueTrigger QuestGiver;
    public NPCDialogueTrigger QuestReturn;
    public Queue<QuestStep> CompletedSteps;
    public List<QuestStep> CompletedList;
    public Queue<QuestStep> StepsQueue;
    public List<QuestStep> StepsList;
    public QuestStep ActiveStep;
    public QuestStep NextStep;

    public void GoToNextStep()
    {
        Initialise();

        if(ActiveStep.isComplete)
        {
            if (CompletedList.Count < StepsList.Count)
            {
                CompletedSteps.Enqueue(ActiveStep);

                if (StepsQueue.Count > 0)
                    ActiveStep = StepsQueue.Dequeue();
                if (StepsQueue.Count > 1)
                    NextStep = StepsQueue.Peek();

                //if (CompletedList.Count >= StepsList.Count)
                //{
                //    isComplete = true;
                //}

            }
            else
            {
                isComplete = true;
                QuestManager.AssignNextQuest();
            }            
        }
        //AssignActiveStep();
    }

    public void Initialise()
    {
        CompletedSteps = new Queue<QuestStep>();
        StepsQueue = new Queue<QuestStep>();
        foreach (QuestStep step in StepsList)
        {
            step.ParentQuest = this;
            StepsQueue.Enqueue(step);
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
            if (!step.isComplete)
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
        CompletedList.Clear();
        CompletedSteps.Clear();
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
