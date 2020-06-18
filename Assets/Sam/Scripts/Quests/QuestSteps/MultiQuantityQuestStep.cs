using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiQuantQuestStep", menuName = "Quest System/MultiQuantQuestStep")]
public class MultiQuantityQuestStep : QuestStep
{
    public List<ItemObj> TargetIDs;
    public List<int> TargetQuantities;
    public List<int> Counters;

    void AddToCounter(string TargetName)
    {
        int index = TargetIDs.FindIndex(i => i.Name == TargetName);
        Counters[index]++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
