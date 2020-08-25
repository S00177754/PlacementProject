using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestManager", menuName = "Quest System/QuestManager")]
public class QuestManager : ScriptableObject
{
    public Quest ActiveMain;

    [SerializeField]
    List<QuestReward> Rewards;

    //[SerializeField]
    //public Quest TrackedQuest;

    [Header("Main Quests")]
    [SerializeField]
    public List<Quest> MainScenarioQuests;
    public List<Quest> FoundMSQuests;

    [Header("Side Quests")]
    [SerializeField]
    public List<Quest> SideQuests;

    public List<Quest> ActiveSides;
    public List<Quest> CompletedSides;


    public void Initialise()
    {
        FoundMSQuests.Clear();
        foreach (Quest main in MainScenarioQuests)
        {
            if (main.isFound)
            {
                FoundMSQuests.Add(main);
            }
            else
            {
                ActiveMain = MainScenarioQuests.First();
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


    public void MoveToNextStepMSQ()
    {
        ActiveMain.GoToNextStep();
    }

    public void LoadSideQuests(){

    }
    public void AssignNextQuest()
    {
        foreach (Quest main in MainScenarioQuests)
        {
            if(!main.isComplete)
            {
                FoundMSQuests.Add(main);
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

