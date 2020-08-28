using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCQuestStep", menuName = "Quest System/Quest Step/NPCQuestStep")]
public class NPCQuestStep : QuestStep
{
    //Quest step to talk to an NPC
    [Header("NPC Steps")]
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;
    public NPCDialogueTrigger TargetNPC;

    //OnTalkTo Event Handle


    public override string GetName()
    {
        return Name;
    }

    public override string GetDescription()
    {
        return Description;
    }
}
