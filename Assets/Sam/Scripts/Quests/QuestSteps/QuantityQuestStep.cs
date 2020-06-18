using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuantityQuestStep", menuName = "Quest System/QuantityQuestStep")]
public class QuantityQuestStep : QuestStep
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
