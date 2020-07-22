using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCQuestStep", menuName = "Quest System/NPCQuestStep")]
public class NPCQuestStep : QuestStep
{
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;
    public NPCDialogueTrigger TargetNPC;

    //OnTalkTo Event Handle

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
