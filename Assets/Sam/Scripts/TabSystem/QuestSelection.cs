﻿using System.Collections;
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
    
    [Header("Required")]
    public GameObject ScrollContent;
    public RectTransform ScrollViewContent;
    public GameObject QuestButtonPrefab;
    public Text QuestName;
    public List<QuestButton> questButtons;

    void OnEnable()
    {
        //Load cuttent Quest list
        Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");
        if (Manager == null)
            Debug.Log("Quest Manager Null");
        foreach (var item in Manager.FoundMSQuests)
        {
            GameObject next = Instantiate(ScrollContent); //Creates new Scrollview Content
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
