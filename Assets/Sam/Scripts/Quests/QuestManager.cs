using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestManager", menuName = "Quest System/QuestManager")]
public class QuestManager : ScriptableObject
{
    [SerializeField]
    public List<Quest> MainScenarioQuests;
    public List<Quest> FoundMSQuests;
    public Quest ActiveMain;

    [SerializeField]
    public List<Quest> SideQuests;

    public List<Quest> ActiveSides;
    public List<Quest> CompletedSides;

    [SerializeField]
    List<QuestReward> Rewards;

    [SerializeField]
    public Quest TrackedQuest;

    public void Initialise()
    {
        foreach (Quest main in MainScenarioQuests)
        {
            if (main.isComplete)
            {
                FoundMSQuests.Add(main);
            }
            else
            {
                ActiveMain = main;
                break;
            }
        }

        foreach (Quest side in SideQuests)
        {
            if (side.isActive)
                ActiveSides.Add(side);
            if (side.isComplete)
                CompletedSides.Add(side);
        }
    }

    void Start()
    {
        //Check next MSQ
        //If the quest is complete, quest is added to found list
        foreach (Quest main in MainScenarioQuests)
        {
            if (main.isComplete)
            {
                FoundMSQuests.Add(main);
            }
            else
            {
                ActiveMain = main;
                break;
            }
        }

        foreach (Quest side in SideQuests)
        {
            if (side.isActive)
                ActiveSides.Add(side);
            if (side.isComplete)
                CompletedSides.Add(side);
        }
    }

    public void MoveToNextStepMSQ(Quest moveOn){
        if(moveOn.isComplete && moveOn.isActive){
            
        }
    }

    public void LoadMSQ(){

    }

    public void LoadSideQuests(){

    }
    public void AssignNextQuest()
    {
        foreach (Quest main in MainScenarioQuests)
        {
            if(!main.isComplete)
            {
                ActiveMain = main;
                break;
            }
        }
    }

    public void SetActiveQuest(Quest setActive){
        setActive.isActive = true;
    }

    public void MarkAsComplete(Quest setComplete){
        setComplete.isComplete = true;
    }
}

