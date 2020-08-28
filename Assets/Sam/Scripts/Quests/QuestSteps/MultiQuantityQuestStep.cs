using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiQuantQuestStep", menuName = "Quest System/Quest Step/MultiQuantQuestStep")]
public class MultiQuantityQuestStep : QuestStep
{
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;
    public List<ItemObj> TargetIDs; //or names here?
    public List<int> TargetQuantities;
    public List<int> Counters;

    void AddToCounter(string TargetName)
    {
        int index = TargetIDs.FindIndex(i => i.Name == TargetName);
        Counters[index]++;
        
        if(TargetQuantities.Sum() == Counters.Sum())
        {
            SetComplete();
            ParentQuest.GoToNextStep();
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
