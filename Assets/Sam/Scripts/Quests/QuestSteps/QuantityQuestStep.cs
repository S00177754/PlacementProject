using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuantityQuestStep", menuName = "Quest System/QuantityQuestStep")]
public class QuantityQuestStep : QuestStep
{
    [Header("Required")]
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;

    [Header("Step Specific")]
    [SerializeField]
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
            isComplete = true;
            ParentQuest.GoToNextStep();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
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
