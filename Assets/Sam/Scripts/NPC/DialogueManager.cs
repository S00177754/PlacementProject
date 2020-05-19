using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static List<Dialogue> MasterDialogue;

    //need to add method to return current dialoge
    //or better to deal with in NPC?
}



public class Dialogue : MonoBehaviour
{
    string StepName;
    Queue<string> CurrentDialogue;
}
