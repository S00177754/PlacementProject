using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward 
{
    protected string Name;
}

public class ExpReward : QuestReward
{
    int ExpAmount;
}

public class ItemReward : QuestReward
{
    ItemObj ItemRewarded;
}


