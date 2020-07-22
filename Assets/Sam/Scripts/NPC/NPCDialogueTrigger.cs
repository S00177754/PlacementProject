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
        ActiveQuest = Resources.Load<QuestManager>("ScriptableObjects/QuestManager").ActiveMain;
        if(ActiveQuest == null)
            ActiveQuest = Resources.Load<QuestManager>("ScriptableObjects/QuestManager").ActiveSides.First();
        CheckDialogue(ActiveQuest.Name);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(activeDialogue);
    }

    public void CheckDialogue(string convoName)
    {

        Debug.Log(convoName);

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
        if (activeDialogue == null)
            SetDefaultConvo();

        //if (activeDialogue != null)
        //    TriggerDialogue();
        //else
        //    Debug.Log("activeDialogue is null in NPCDTrigger");
    }

    private void SetDefaultConvo()
    {
        activeDialogue = conversations.Where(d => d.conversationName.Equals("default")).FirstOrDefault();
    }

}
