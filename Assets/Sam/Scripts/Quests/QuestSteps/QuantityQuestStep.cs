﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuantityQuestStep", menuName = "Quest System/Quest Step/QuantityQuestStep")]
public class QuantityQuestStep : QuestStep
{
    [Header("Required")]
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;

    [Header("Step Specific")]
    [SerializeField]
    public ItemObj targetObject;
    public string TargetName;
    //public int TargetID;
    public int TargetQuantity;
    public int TargetObtained;


    public void AddToCounter()
    {
        //check inventory
        TargetObtained++;
        if (TargetObtained >= TargetQuantity)
        {
            SetComplete();
            ParentQuest.GoToNextStep();
        }
    }

    public string GetItemName()
    {
        return TargetName;
    }
    public override string GetName()
    {
        return Name;
    }

    public override string GetDescription()
    {
        return Description;
    }
}
