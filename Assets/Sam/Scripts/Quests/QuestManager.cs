using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    Queue<Quest> MainScenarioQuests;
    [SerializeField]
    List<Quest> SideQuests;
    [SerializeField]
    List<Quest> ActiveSides;

    List<QuestReward> Rewards;

    public void MoveToNextStepMSQ(Quest moveOn){
        if(moveOn.isComplete && moveOn.isActive){

        }
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
