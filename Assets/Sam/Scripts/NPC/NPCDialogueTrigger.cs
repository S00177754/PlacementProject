using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public List<Dialogue> conversations;
    public Dialogue activeDialogue;

    public void TriggerDialogue()
    {
        CheckDialogue("");
        FindObjectOfType<DialogueManager>().StartDialogue(activeDialogue);
    }

    private void CheckDialogue(string convoName)
    {
        //Clear dialogue
        activeDialogue = new Dialogue();
        //Use input to determine which dialoge to use
        //Tied to quest name
        foreach (Dialogue dialogue in conversations)
        {
            if(dialogue.conversationName.Equals(convoName))
            {
                activeDialogue = dialogue; 
            }
        }
        if (activeDialogue == null)
            SetDefaultConvo();
    }

    private void SetDefaultConvo()
    {
        activeDialogue = conversations.Where(d => d.name.Equals("default")).FirstOrDefault();
    }

}
