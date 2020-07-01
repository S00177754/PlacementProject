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

    public List<QuestButton> questButtons;
    public RectTransform ScrollViewTransfrom;
    public GameObject QuestButtonPrefab;
    public Text QuestName;

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


        if (CompareTag("Main Scenario"))
            PopulateButtons(Manager.FoundMSQuests);
        else if (CompareTag("SideQuest"))
            PopulateButtons(Manager.ActiveSides);



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

    public void PopulateButtons(List<Quest> PopulationContent)
    {
        foreach (Quest msquest in PopulationContent)
        {
            GameObject next = Instantiate(ScrollContent); //Creates new Scrollview Content
            next.SetActive(true);

            next.transform.SetParent(ScrollViewTransfrom.transform.parent, false);
            QuestName = next.GetComponentInChildren<Text>();
            if (QuestName != null)
            {
                QuestName.text = msquest.Name;
                QuestName.color = Color.black;
                Debug.Log(QuestName);
                Debug.Log("Quest added: " + msquest.Name);
            }
            else
                Debug.Log("QuestName is null");
        }
    }

}
