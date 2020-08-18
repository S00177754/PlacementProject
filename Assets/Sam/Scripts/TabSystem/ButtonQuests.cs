using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ButtonQuests : MonoBehaviour
{

    public GameObject Panel;
    
    public Quest buttonQuest;


    private string stepListAsText;


    public void QuestButtonClick()
    {
        Panel.SetActive(true);
        TMP_Text DisplayText = Panel.GetComponentInChildren<TMP_Text>();
        DisplayText.text = "";
        stepListAsText = "";
        foreach (QuestStep step in buttonQuest.StepsList)
        {
            if(step.isComplete)
                stepListAsText += string.Format("<color=\"red\"><alpha=#DD>" + step.GetDescription() + "\n");
            else
            {
                stepListAsText += string.Format("<color=\"black\"><alpha=#FF>" + step.GetDescription() + "\n");
                break;
            }
        }

        DisplayText.text = stepListAsText;
       
    }

    public void ClearMe()
    {
        Destroy(gameObject);
    }

}
