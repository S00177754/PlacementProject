using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC", order = 0)]
public class NPC : ScriptableObject
{
    int ID;
    [SerializeField]
    public string Name;
    public string Description;
    public Vector3 Location;
    [SerializeField]
    public string DefaultSpeach;

    public List<string> CheckQuestDialogue(){

        List<string> myDialogue = new List<string>();
        //Refer to QuestManager to get active quests
        //if active quest has NPC, check name/id with self
        //if valid selection
        //run quesry on DialogueManager MasterList
        //check activequest step name with name of dialogue option
        //set Dialogue to dialoge returned from query
        
        //defalut to DefaultSpeach

        //return dialogue
        return myDialogue;
    }

}
