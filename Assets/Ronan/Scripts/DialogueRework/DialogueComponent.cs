using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueComponent : MonoBehaviour
{
    public static Dictionary<string, DialogueComponent> DialogueSpeakers;
    public static bool HasRequestedLoad = false;

    [Header("Options")]
    public bool Interactable = true;

    [Header("NPC Info")]
    public string NPCName;
    public string ID;

    [Header("Dialogue")]
    public List<DialogueBlock> DefaultConversations;
    public List<DialogueBlock> Conversations;


    public void Awake()
    {
        if (!DialogueSpeakers.ContainsKey(ID))
        {
            DialogueSpeakers.Add(ID, this);
        }
        else
        {
            Debug.LogError(string.Concat("Duplicate Dialogue Speaker! Object: ", gameObject.name, " Location:", transform.position.ToString()));
            Interactable = false;
        }
    }

    public void Start()
    {
        if(!HasRequestedLoad)
        {
            HasRequestedLoad = true;
            //Get SaveLoad to assign data
        }
    }

    public void Speak()
    {
        DialogueBlock dialogue = new DialogueBlock();
        for (int i = 0; i < Conversations.Count; i++)
        {
            if (!Conversations[i].HasBeenSeen)
            {
                dialogue = Conversations[i];
                break;
            }
        }

        if (dialogue == null)
            dialogue = GetRandomDefault();

        //Show dialogue on screen
    }

    //Helper Methods
    public DialogueBlock GetRandomDefault()
    {
        if (DefaultConversations.Count > 0)
        {
            return DefaultConversations[UnityEngine.Random.Range(0, DefaultConversations.Count)];
        }
        else
        {
            return null;
        }
    }


    //Trigger
    private void OnTriggerEnter(Collider other)
    {
        //Set as active speaker
    }


    //Persistence
    public List<DialogueData> GetDialogueData()
    {
        List<DialogueData> data = new List<DialogueData>();
        foreach (var block in Conversations)
        {
            data.Add(new DialogueData()
            {
                ConversationName = block.ConversationName,
                OwnerID = block.OwnerID,
                HasBeenSeen = block.HasBeenSeen
            });
        }
        return data;
    }

    public void AssignDialogueData(DialogueData data)
    {
            foreach (var gameBlock in Conversations)
            {
                if(data.ConversationName == gameBlock.ConversationName)
                {
                    gameBlock.HasBeenSeen = data.HasBeenSeen;
                }

            }
    }

}

[Serializable]
public class DialogueBlock
{
    public string OwnerID;

    public string ConversationName;

    public bool HasBeenSeen = false;

    [TextArea(3, 10)]
    public string[] Sentences; 
}

[Serializable]
public class DialogueData
{
    public string OwnerID;
    public string ConversationName;
    public bool HasBeenSeen;
}
