using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour    //, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public QuestSelection questGroup;
    public Quest myQuest;
    public TMP_Text QuestNameText;
    public TMP_Text QuestStepTextObject;
    public bool isSelected;
    public GameObject MyPannel;
    public GameObject CompletedStep;
    public GameObject CurrentStep;


    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    questGroup.OnQuestSelected(this);
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    questGroup.OnQuestEnter(this);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    questGroup.OnQuestExit(this);
    //}

    public void OnClick()
    {
        //use myQuest
        QuestNameText.text = myQuest.name;
        //Place questStep complete stylised
        myQuest.CheckCompletedSteps(); //gets most up to date info on quest status
        foreach (QuestStep step in myQuest.CompletedList)
        {
            //ADD stylised text to scrollview
            if (CompletedStep.TryGetComponent( out QuestStepTextObject))
            {
                QuestStepTextObject.text = string.Format("<bold>" + step.name + "</bold>\n<indent=5%>" + step.GetDescription() + "</indent>");
            }
        }

        //place current queststep stylised
        //

        //use description for step details

    }

    void Start()
    {
        //QuestNameText = GetComponent<Text>();
        //questGroup.Subscribe(this);
        //isSelected = false;
        //MyPannel.SetActive(false);
    }

    void OnEnable()
    {
        QuestNameText = GetComponent<TMP_Text>();
        questGroup.Subscribe(this);
        isSelected = false;
        MyPannel.SetActive(false);
    }

    void CheckCompletedSteps()
    {
        foreach (QuestStep step in myQuest.StepsList)
        {

        }
    }

}
