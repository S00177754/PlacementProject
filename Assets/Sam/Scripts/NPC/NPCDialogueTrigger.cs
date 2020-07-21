using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public List<Dialogue> conversations;
    public Dialogue activeDialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(activeDialogue);
    }

    private void CheckDialogue()
    {
        //Use input to determine which dialoge to use
        //Tied to quest name
    }

}
