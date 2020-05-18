using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStep : MonoBehaviour
{
    int ID;
    string Description;
    bool isComplete;


}

public class LocationQuestStep : QuestStep
{
    Vector3 StepLocation;
}

public class NPCQuestStep : QuestStep
{
    NPC TargetNPC;

}

public class QuantityQuestStep : QuestStep
{
    int TargetID;
    int TargetQuantity;
    int counter;

    void AddToCounter(){
        counter ++;
    }
}

public class MultiQuantityQuestStep : QuestStep
{
    List<int> TargetIDs;
    List<int> TargetQuantities;
    List<int> Counters;

    void AddToCounter(int ID){
        int index = TargetIDs.FindIndex(i => i == ID);
        Counters[index]++;
    }
}
