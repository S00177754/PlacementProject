using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuantityQuestStep", menuName = "Quest System/QuantityQuestStep")]
public class QuantityQuestStep : QuestStep
{
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;

    public int TargetID;
    public string TargetName;
    public int TargetQuantity;
    public int counter;

    public void AddToCounter()
    {
        counter++;
        if (counter >= TargetQuantity)
        {
            isComplete = true;
            //Do other stuff
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
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
