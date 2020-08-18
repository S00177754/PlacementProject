using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ButtonQuests : MonoBehaviour
{
    QuestManager Manager;

    public GameObject Panel;
    //public Transform QuestStepTransform;
    
    public Quest buttonQuest;


    private string stepListAsText;
    private StepListController StepController;

    private void Start()
    {

    }


    public void QuestButtonClick()
    {
        Panel.SetActive(true);
        TMP_Text DisplayText = Panel.GetComponentInChildren<TMP_Text>();
        DisplayText.text = "";
        stepListAsText = "";
        foreach (QuestStep step in buttonQuest.StepsList)
        {
            if(step.isComplete)
                stepListAsText += string.Format(step.GetDescription() + "\n");
            else
            {
                stepListAsText += string.Format(step.GetDescription() + "\n");
                break;
            }
        }

        DisplayText.text = stepListAsText;
        DisplayText.alpha = 1.0f;




        //StepController = QuestStepPrefab.GetComponent<StepListController>();
        //if (StepController == null)
        //    Debug.Log("ButtonQuest: stepcontroller is null");
        //if (buttonQuest == null)
        //    Debug.Log("ButtonQuest: buttonQuest is null");

        //StepController.SendQuest(buttonQuest);
        //StepController.FillSteps();


        //GameObject instance = Instantiate(StepListPrefab, StepsPanel.transform);

        //string textToDisplay = "";
        //stepListText = StepListPrefab.GetComponentInChildren<TMP_Text>();
        //stepListText.text = string.Empty;

        //if (stepListText == null)
        //    Debug.Log("ButtonQuest: stepListText is null");

        //foreach (QuestStep step in buttonQuest.StepsList)
        //{
        //    if (step.isComplete)
        //    {
        //        textToDisplay += string.Format(step.GetDescription() + "\\n");
        //    }
        //    else
        //    {
        //        textToDisplay += string.Format(step.GetDescription() + "\\n");
        //        break;
        //    }
        //}
        //stepListText.text = textToDisplay;
        //stepListText.alpha = 1.0f;
        //stepListText.color = Color.white;

        //FillQuestSteps();
    }

    //public void FillQuestSteps()
    //{
    //    GameObject instance = Instantiate(QuestStepPrefab, StepScrollContent.transform);

    //    TMP_Text questDescription = instance.GetComponent<TMP_Text>();
    //    questDescription.text = buttonQuest.Name;

    //    foreach (QuestStep step in buttonQuest.StepsList)
    //    {
    //        if (step.isComplete)
    //        {
    //            instance = Instantiate(QuestStepPrefab, instance.transform);
    //            questDescription = instance.GetComponent<TMP_Text>();
    //            questDescription.text = step.GetDescription();
    //            questDescription.color = Color.red;
    //            questDescription.alpha = 0.9f;
    //        }
    //        else
    //        {
    //            instance = Instantiate(QuestStepPrefab, instance.transform);
    //            questDescription = instance.GetComponent<TMP_Text>();
    //            questDescription.text = step.GetDescription();
    //            questDescription.color = Color.black;
    //            questDescription.alpha = 1.0f;
    //            break;
    //        }
    //    }



    //}

    public void ClearMe()
    {
        Destroy(gameObject);
    }



    //private void Start()
    //{
    //    Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");
    //    buttonText = GetComponent<TMP_Text>();
    //    if (tag.Equals("Main Scenario"))
    //        buttonQuest = Manager.FoundMSQuests.Where(q => q.Name.Equals(buttonText.text)) as Quest;
    //    else if (tag.Equals("Side Quest"))
    //        buttonQuest = Manager.SideQuests.Where(q => q.Name.Equals(buttonText.text)) as Quest;

    //    Debug.Log(buttonQuest.Name);

    //}

    //private void OnEnable()
    //{
    //    Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");
    //    buttonText = GetComponent<TMP_Text>();
    //    if (tag.Equals("Main Scenario"))
    //        buttonQuest = Manager.FoundMSQuests.Where(q => q.Name.Equals(buttonText.text)) as Quest;
    //    else if (tag.Equals("Side Quest"))
    //        buttonQuest = Manager.SideQuests.Where(q => q.Name.Equals(buttonText.text)) as Quest;
    //}

    
}
