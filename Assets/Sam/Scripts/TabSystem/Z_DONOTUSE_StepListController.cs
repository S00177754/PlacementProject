using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepListController : MonoBehaviour
{
    public GameObject StepListPrefab;

    public GameObject StepListContent;

    private Transform stepTransform;
    private List<GameObject> CurrentSteps;
    private Quest buttonQuest;

    public void FillSteps()
    {
        stepTransform = StepListContent.transform;
        TMP_Text TMPText;
        foreach (QuestStep step in buttonQuest.StepsList)
        {
            GameObject text = Instantiate(StepListPrefab, stepTransform);
            TMPText = text.GetComponent<TMP_Text>();
            if(step.isComplete)
            {
                TMPText.text = step.GetDescription();
                TMPText.color = Color.red;
                CurrentSteps.Add(text);
            }
            else
            {
                TMPText.text = step.GetDescription();
                TMPText.color = Color.black;
                CurrentSteps.Add(text);
                break;
            }
        }
    }

    public void SendQuest(Quest questFromButton)
    {
        buttonQuest = questFromButton;
    }

    void Start()
    {
        CurrentSteps = new List<GameObject>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
