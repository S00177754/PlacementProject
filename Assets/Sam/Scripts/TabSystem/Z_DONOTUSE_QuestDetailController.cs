using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDetailController : MonoBehaviour
{
    public GameObject QuestStepPrefab;

    public GameObject StepScrollContent;

    //public Quest ButtonQuest;

    //private TMP_Text StepText;
    private List<GameObject> CurrentSteps;

    private Quest buttonQuest;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSteps = new List<GameObject>();
    }


    public void FillStepList()
    {
        //questDescription.text = StepText.text;

        foreach (QuestStep step in buttonQuest.StepsList)
        {
            GameObject instance = Instantiate(QuestStepPrefab, StepScrollContent.transform);
            
            TMP_Text questDescription = instance.GetComponent<TMP_Text>();
            if (step.isComplete)
            {
                //instance = Instantiate(QuestStepPrefab, StepScrollContent.transform);
                questDescription = instance.GetComponent<TMP_Text>();
                questDescription.text = step.GetDescription();
                questDescription.color = Color.red;
                questDescription.alpha = 0.9f;
                CurrentSteps.Add(instance);
            }
            else
            {
                //instance = Instantiate(QuestStepPrefab, StepScrollContent.transform);
                questDescription = instance.GetComponent<TMP_Text>();
                questDescription.text = step.GetDescription();
                questDescription.color = Color.black;
                questDescription.alpha = 1.0f;
                CurrentSteps.Add(instance);
                break;
            }
        }


    }

    public void SendQuest(Quest questFromButton)
    {
        buttonQuest = questFromButton;
    }

    public void ClearSteps()
    {
        foreach (var item in CurrentSteps)
        {
            Destroy(item);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
