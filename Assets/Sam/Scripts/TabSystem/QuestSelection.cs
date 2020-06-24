using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Slight Inheritance from TabGroup, 
//variables and methods used with additional functionality
//but require ref to QuestButton, inheriting from TabButton
public class QuestSelection : TabGroup
{
    QuestManager Manager;
    public GameObject ScrollContent;
    public Text QuestName;
    public List<QuestButton> questButtons;

    void Start()
    {
        //Load cuttent Quest list
        Manager = Resources.Load<QuestManager>("Sam/ScriptableObjects/QuestManager");

        foreach (var item in Manager.FoundMSQuests)
        {
            GameObject next = Instantiate(ScrollContent);
            next.TryGetComponent<Text>(out QuestName);
            if (QuestName != null)
                QuestName.text = item.Name;
        }

        tabIdleColor = new Color(0, 0, 0, 1);
        tabHoverColor = new Color(0, 111, 255, 1);
        tabActiveColor = new Color(0, 255, 0, 1);
    }

    public void Subscribe(QuestButton questButton)
    {
        if (questButtons == null)
        {
            questButtons = new List<QuestButton>();
            tabCanveses = new List<Canvas>();
        }

        questButtons.Add(questButton);
        tabCanveses.Add(questButton.MyCanvas);
    }

    public void OnQuestSelected(QuestButton questButton)
    {
        //Assign colours
        OnTabSelected(questButton);

        //Update quest name
    }

    public void OnQuestEnter(QuestButton questButton)
    {
        OnTabEnter(questButton);
    }

    public void OnQuestExit(QuestButton questButton)
    {
        OnTabExit(questButton);
    }

}
