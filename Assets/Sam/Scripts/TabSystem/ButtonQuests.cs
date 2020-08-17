using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ButtonQuests : MonoBehaviour
{
    QuestManager Manager;

    //public GameObject QuestStepPrefab;

    //public GameObject StepScrollContent;

    public GameObject StepListPrefab;

    //private TMP_Text buttonText;
    
    public Quest buttonQuest;

    private QuestDetailController StepController;




    public void QuestButtonClick()
    {
        StepController = StepListPrefab.GetComponentInChildren<QuestDetailController>();
        StepController.ClearSteps();
        StepController.FillStepList(buttonQuest);
        //FillQuestSteps();
    }



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

    //public void FillQuestSteps()
    //{
    //    GameObject instance = Instantiate(QuestStepPrefab, StepScrollContent.transform);
    //    TMP_Text questDescription = instance.GetComponent<TMP_Text>();
    //    questDescription.text = buttonText.text;

    //    foreach (QuestStep step in buttonQuest.Steps)
    //    {
    //        if (step.isComplete)
    //        {
    //            instance = Instantiate(QuestStepPrefab, StepScrollContent.transform);
    //            questDescription = instance.GetComponent<TMP_Text>();
    //            questDescription.text = step.GetDescription();
    //            questDescription.color = Color.red;
    //            questDescription.alpha = 0.9f;
    //        }
    //        else
    //        {
    //            instance = Instantiate(QuestStepPrefab, StepScrollContent.transform);
    //            questDescription = instance.GetComponent<TMP_Text>();
    //            questDescription.text = step.GetDescription();
    //            questDescription.color = Color.black;
    //            questDescription.alpha = 1.0f;
    //            break;
    //        }
    //    }



    //}
}
