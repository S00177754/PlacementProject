using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward 
{
    protected string Name;
}

public class ExpReward : QuestReward
{
    float ExpAmount;

    //Allows exp to be added from another script without having access to 
    //ExpAmount
    public float GiveExp(){
        return ExpAmount;
    }
}

public class ItemReward : QuestReward
{
    string ItemName;
    ItemObj ItemRewarded;

    public void GetItemFromName(string item){
       //Search DB of items and assign reward
    } 
}


