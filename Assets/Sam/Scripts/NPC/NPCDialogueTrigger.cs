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
    public bool canTalk;
    [SerializeField]
    DialogueManager DialogueManager;
    [SerializeField]
    Quest ActiveQuest;
    [SerializeField]
    QuestManager QuestManage;



    private void Start()
    {
        //Initiates to find active quest and finds current dialogue option for that
        CheckForQuest();
        CheckDialogue(ActiveQuest.Name);

        Location = this.gameObject.transform.position;
        
        if (conversations.Count > 0)
            canTalk = true;
        else
            canTalk = false;
        //Conversation manager subscribe NPC to 'talkable'
        FindObjectOfType<DialogueManager>().Subscribe(this);
    }

    public void TriggerDialogue()
    {
        //Checks if dialogue has updated from quests then starts in Manager
        CheckDialogue(ActiveQuest.Name);
        if(ActiveQuest.ActiveStep.GetType() == typeof(NPCQuestStep))
        {
            ActiveQuest.ActiveStep.isComplete = true;
            ActiveQuest.GoToNextStep();
        }
        
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

    public void CheckForHandIn()
    {
        int questIndex = ActiveQuest.StepsList.IndexOf(ActiveQuest.ActiveStep);

        if (ActiveQuest.StepsList[questIndex - 1].GetType() == typeof(QuantityQuestStep))
        {
            //PlayerController.Instance.GetComponent<InventoryObj>().RemoveItem();   
        }
    }

    private void CheckForQuest()
    {
        //Checks Active Quest and finds conversation of same name.
        ActiveQuest = QuestManage.ActiveMain;
        
        if (ActiveQuest == null)
        {
            Debug.Log("ActiveQuest null in Start()");
            ActiveQuest = QuestManage.ActiveSides.First();
            if (ActiveQuest == null)
                Debug.Log("ActiveQuest null in Start() after side search");
        }
        
    }

    private void MarkComplete()
    {
        ActiveQuest.isComplete = true;
        ActiveQuest.GoToNextStep();
    }

    private void OnTriggerEnter(Collider other)
    {
        canTalk = true;
        DialogueManager.ActiveNPC = this;
    }

    private void OnTriggerExit(Collider other)
    {
        canTalk = false;
    }

}
