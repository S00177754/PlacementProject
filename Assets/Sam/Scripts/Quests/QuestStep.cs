using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStep", menuName = "~/Documents/GitHub/PlacementProject/Assets/Sam/Scripts/Quests/QuestStep.cs/QuestStep", order = 0)]
public class QuestStep : ScriptableObject {
    
    protected int ID;
    [SerializeField]
    protected string Description;
    protected bool isComplete;
}

[CreateAssetMenu(fileName = "LocationQuestStep", menuName = "Quest System/QuestStep")]
public class LocationQuestStep : QuestStep
{
    //Assign to empty
    Vector3 StepLocation;

    private void OnTriggerEnter(Collider other) {
        //
    }
}

public class NPCQuestStep : QuestStep
{
    NPC TargetNPC;

    //OnTalkTo Event Handle
}

public class QuantityQuestStep : QuestStep
{
    int TargetID;
    string TargetName;
    int TargetQuantity;
    int counter;

    void AddToCounter(){
        counter ++;
        if(counter >= TargetQuantity){
            isComplete = true;
            //Do other stuff
        }
    }
}

public class MultiQuantityQuestStep : QuestStep
{
    List<ItemObj> TargetIDs;
    List<int> TargetQuantities;
    List<int> Counters;

    void AddToCounter(string TargetName){
        int index = TargetIDs.FindIndex(i => i.Name == TargetName);
        Counters[index]++;
    }
}
