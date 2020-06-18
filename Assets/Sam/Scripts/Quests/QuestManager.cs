using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestManager", menuName = "Quest System/QuestManager")]
public class QuestManager : ScriptableObject
{
    [SerializeField]
    List<Quest> MainScenarioQuests;
    [SerializeField]
    List<Quest> SideQuests;
    [SerializeField]
    List<Quest> ActiveSides;
    [SerializeField]
    List<QuestReward> Rewards;

    public void MoveToNextStepMSQ(Quest moveOn){
        if(moveOn.isComplete && moveOn.isActive){
            
        }
    }

    public void LoadMSQ(){
        //read in quests from JSON?
    }

    public void LoadSideQuests(){
        //read from JSON?
    }
    public void CheckNextQuest(){

    }

    public void SetAvtiveQuest(Quest setActive){
        setActive.isActive = true;
    }

    public void MarkAsComplete(Quest setComplete){
        setComplete.isComplete = true;
    }
}

