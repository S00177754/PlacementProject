using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    static public Dictionary<string, NPCDialogueTrigger> ChattyNPCs = new Dictionary<string, NPCDialogueTrigger>();

    public Animator animator;
    public TMP_Text nameText;
    public TMP_Text dialoguetext;

    public NPCDialogueTrigger ActiveNPC;
    GameStateController gameStateController;

    private Queue<string> scentences;

    void Start()
    {
        //scentences = new Queue<string>();
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();

    }

    public void Subscribe(NPCDialogueTrigger addMe)
    {
        ChattyNPCs.Add(addMe.name, addMe);
    }

    public void InteractButtonPress()
    {
        ActiveNPC.TriggerDialogue();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Starts dialogue by showing dialogue panel and filling in name and first scentence
        animator.SetBool("IsOpen", true);
        Debug.Log("Start Conversation with " + dialogue.name);

        nameText.text = ActiveNPC.name;

        scentences.Clear();

        foreach (string scentence in dialogue.scentances)
            scentences.Enqueue(scentence);

        DisplayNextScentance();
        
    }

    public void DisplayNextScentance()
    {
        //As scentence Queue empties, EndDialogue is called to finish on screen
        if(scentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Allows panel to fill in next scentence if player clicks fast
        StopAllCoroutines();
        //dialoguetext.text = scentences.Dequeue();
        //Begins typing scentence into screen
        StartCoroutine(TypeScentence(scentences.Dequeue()));
    }

    IEnumerator TypeScentence (string scentence)
    {
        //Could set different delay for faster/slower text diaplay
        dialoguetext.text = "";
        foreach (char letter in scentence.ToCharArray())
        {
            dialoguetext.text += letter;
            yield return null;
        }
    }    

    void EndDialogue()
    {
        //Resets animator to close dialogue box
        //ActiveNPC.CheckForHandIn();
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
        GameStateController.SetGameState(GameState.Explore);
        
    }

    public void SetActiveNPC(string name)
    {
        ActiveNPC = ChattyNPCs[name];
    }
}
