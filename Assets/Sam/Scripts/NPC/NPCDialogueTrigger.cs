using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public string NPCName;
    public string Description;
    public Vector3 Location;
    public List<Dialogue> conversations;
    public Dialogue activeDialogue;

    Quest ActiveQuest;

    private void Start()
    {
        //activeDialogue = new Dialogue();

        CheckForQuest();
        CheckDialogue(ActiveQuest.Name);
        //****DEBUG****
        //Debug.Log(activeDialogue.name);
        //foreach (string scen in activeDialogue.scentances)
        //{
        //    Debug.Log(scen);
        //}
    }

    public void TriggerDialogue()
    {
        CheckDialogue(ActiveQuest.Name);
        FindObjectOfType<DialogueManager>().StartDialogue(activeDialogue);
    }

    public void CheckDialogue(string convoName)
    {
        //****DEBUG****
        //Debug.Log(convoName);

        CheckForQuest();
        //Clear dialogue
        activeDialogue = new Dialogue();

        //Use input to determine which dialoge to use
        foreach (Dialogue dialogue in conversations)
        {
            if(dialogue.conversationName.Equals(convoName))
            {
                activeDialogue = dialogue;
            }
        }

        if (activeDialogue.name == null)
            CheckDialogue("default");
    }

    private void CheckForQuest()
    {
        ActiveQuest = Resources.Load<QuestManager>("ScriptableObjects/QuestManager").ActiveMain;
        if (ActiveQuest == null)
        {
            Debug.Log("ActiveQuest null in Start()");
            ActiveQuest = Resources.Load<QuestManager>("ScriptableObjects/QuestManager").ActiveSides.First();
            if (ActiveQuest == null)
                Debug.Log("ActiveQuest null in Start() after side search");
        }
        
    }

}
