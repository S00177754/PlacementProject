using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public TMP_Text nameText;
    public TMP_Text dialoguetext;

    private Queue<string> scentences;

    void Start()
    {
        scentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //****DEBUG****
        //if (dialogue == null)
        //    Debug.Log("dialogue is null in DiagManager");
        //else
        //{
        //    Debug.Log(dialogue.name);
        //    Debug.Log(dialogue.conversationName);
        //    Debug.Log(dialogue.scentances);
        //}

        //Starts dialogue by showing dialogue panel and filling in name and first scentence
        animator.SetBool("IsOpen", true);
        Debug.Log("Start Conversation with " + dialogue.name);

        nameText.text = dialogue.name;

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
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
    }


}
