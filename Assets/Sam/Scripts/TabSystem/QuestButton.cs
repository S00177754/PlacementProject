using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestButton : TabButton
{
    public QuestSelection questGroup;
    public Quest myQuest;
    public Text QuestNameText;



    public new void OnPointerClick(PointerEventData eventData)
    {
        questGroup.OnQuestSelected(this);
    }

    public new void OnPointerEnter(PointerEventData eventData)
    {
        questGroup.OnQuestEnter(this);
    }

    public new void OnPointerExit(PointerEventData eventData)
    {
        questGroup.OnQuestExit(this);
    }

    void Start()
    {
        QuestNameText = GetComponent<Text>();
        questGroup.Subscribe(this);
        isSelected = false;
        MyPannel.SetActive(false);

        
        //else if(tag.Equals("Side Quest"))
        //{
        //    //Logic for side quests
        //}
    }
}
