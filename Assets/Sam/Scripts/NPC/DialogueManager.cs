using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public Text nameText;
    public Text dialoguetext;

    private Queue<string> scentences;

    void Start()
    {
        scentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
            Debug.Log("dialogue is null in DiagManager");


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
        if(scentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        //dialoguetext.text = scentences.Dequeue();
        StartCoroutine(TypeScentence(scentences.Dequeue()));
    }

    IEnumerator TypeScentence (string scentence)
    {
        dialoguetext.text = "";
        foreach (char letter in scentence.ToCharArray())
        {
            dialoguetext.text += letter;
            yield return null;
        }
    }    

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
    }


}
