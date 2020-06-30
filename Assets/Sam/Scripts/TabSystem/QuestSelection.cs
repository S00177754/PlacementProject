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
    
    public RectTransform ScrollViewContent;
    public GameObject QuestButtonPrefab;
    public Text QuestName;
    public List<QuestButton> questButtons;

    [Header("Required")]
    public GameObject ScrollContent;

    void Start()
    {
        //Load cuttent Quest list
        Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");

        //Debugging
        if (Manager == null)
            Debug.Log("Quest Manager Null");
        if (Manager.FoundMSQuests == null)
            Debug.Log("FoundMSQuest is null");
        else if (Manager.FoundMSQuests.Count == 0)
            Debug.Log("FoundMSQuest not null count = 0");
        else if (Manager.FoundMSQuests.Count > 0)
            Debug.Log(string.Format("FoundMSQuests contains {0} elements", Manager.FoundMSQuests.Count));

        foreach (var item in Manager.FoundMSQuests)
        {
            GameObject next = Instantiate(ScrollContent); //Creates new Scrollview Content
            next.TryGetComponent<Text>(out QuestName);
            if (QuestName != null)
            {
                QuestName.text = item.Name;
                Debug.Log(QuestName);
            }
            Debug.Log("Quest added: " + item.Name);
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
            tabPannels = new List<GameObject>();
        }

        questButtons.Add(questButton);
        tabPannels.Add(questButton.MyPannel);
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
