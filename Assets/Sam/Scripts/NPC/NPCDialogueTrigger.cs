using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public string NPCName;
    public string Description;
    public Vector3 Location;
    public List<Dialogue> conversations;
    public Dialogue activeDialogue;

    [SerializeField]
    Quest ActiveQuest;



    private void Start()
    {
        //Initiates to find active quest and finds current dialogue option for that
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
        //Checks if dialogue has updated from quests then starts in Manager
        CheckDialogue(ActiveQuest.Name);
        FindObjectOfType<DialogueManager>().StartDialogue(activeDialogue);
    }

    public void CheckDialogue(string convoName)
    {
        //Checks active quest then assigns dialogue 
        //If no matching conversation has quest name, default dialogue is applied
        CheckForQuest();
        //Clear dialogue
        activeDialogue = new Dialogue();

        //Will only search if NPC has multiple conversations
        if (conversations.Count > 1)
        {
            foreach (Dialogue dialogue in conversations)
            {
                if (dialogue.conversationName.Equals(convoName))
                {
                    activeDialogue = dialogue;
                }
            }
            if (activeDialogue.name == null)
                CheckDialogue("default");
        }
        else
            activeDialogue = conversations[0];

    }

    private void CheckForQuest()
    {
        //Checks Active Quest and finds conversation of same name.
        //TODO: Will need to be step specific for returning to same NPC to turn in quest
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
