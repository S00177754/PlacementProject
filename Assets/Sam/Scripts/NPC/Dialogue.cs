using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //NPC Name
    public string name;
    //Conversation Name eg default, defaultTalked etc
    public string conversationName;
    //Used to detemine default 
    public bool hasBeenSeen;
    //Max char = 260
    [TextArea(3,10)]
    public string[] scentances;
}
