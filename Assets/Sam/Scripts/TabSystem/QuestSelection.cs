using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestSelection : MonoBehaviour
{
    QuestManager Manager;

    public List<QuestButton> questButtons;
    private List<GameObject> tabPannels;
    public RectTransform ScrollViewTransfrom;
    public GameObject QuestButtonPrefab;
    [SerializeField]
    public GameObject ActivePannel;
    public Text QuestName;

    [SerializeField]
    private Color tabIdleColor;
    [SerializeField]
    private Color tabHoverColor;
    [SerializeField]
    private Color tabActiveColor;
    [Header("Required")]
    public GameObject ScrollContent;



    void Start()
    {
        //Load cuttent Quest list
        Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");
        tabPannels = new List<GameObject>();
        //Debugging
        //if (Manager == null)
        //    Debug.Log("Quest Manager Null");
        if (Manager.FoundMSQuests == null)
            Debug.Log("FoundMSQuest is null");
        else if (Manager.FoundMSQuests.Count == 0)
            Debug.Log("FoundMSQuest not null count = 0");
        else if (Manager.FoundMSQuests.Count > 0)
            Debug.Log(string.Format("FoundMSQuests contains {0} elements", Manager.FoundMSQuests.Count));

        if (Manager != null)
        {
            if (CompareTag("Main Scenario"))
                PopulateButtons(Manager.FoundMSQuests);
            else if (CompareTag("Side Quest"))
                PopulateButtons(Manager.ActiveSides);
            else
                Debug.Log("Quest Selection Tag error");
        }
        else
            Debug.Log("Manager is null");


        tabIdleColor = new Color(0, 0, 0, 1);
        tabHoverColor = new Color(0, 111, 255, 1);
        tabActiveColor = new Color(0, 255, 0, 1);
    }

    private void OnEnable()
    {
        ////Load cuttent Quest list
        //Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");
        //tabPannels = new List<GameObject>();
        ////Debugging
        ////if (Manager == null)
        ////    Debug.Log("Quest Manager Null");
        //if (Manager.FoundMSQuests == null)
        //    Debug.Log("FoundMSQuest is null");
        //else if (Manager.FoundMSQuests.Count == 0)
        //    Debug.Log("FoundMSQuest not null count = 0");
        //else if (Manager.FoundMSQuests.Count > 0)
        //    Debug.Log(string.Format("FoundMSQuests contains {0} elements", Manager.FoundMSQuests.Count));

        //if (Manager != null)
        //{
        //    if (CompareTag("Main Scenario"))
        //        PopulateButtons(Manager.FoundMSQuests);
        //    else if (CompareTag("Side Quest"))
        //        PopulateButtons(Manager.ActiveSides);
        //    else
        //        Debug.Log("Quest Selection Tag error");
        //}
        //else
        //    Debug.Log("Manager is null");

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
        ResetTabs();
        questButton.isSelected = !questButton.isSelected;
        ActivePannel = questButton.MyPannel;

        foreach (QuestButton button in questButtons)
        {
            if (button.isSelected)
                button.QuestNameText.color = tabActiveColor;
        }
        SwitchPannel();
        //Update quest name
    }

    public void OnQuestEnter(QuestButton questButton)
    {
        if (questButton.isSelected)
            questButton.QuestNameText.color = tabActiveColor;
        else
            questButton.QuestNameText.color = tabHoverColor;
    }

    public void OnQuestExit(QuestButton questButton)
    {
        if (questButton.isSelected)
            questButton.QuestNameText.color = tabActiveColor;
        else
            questButton.QuestNameText.color = tabIdleColor;
    }

    public void PopulateButtons(List<Quest> PopulationContent)
    {
        foreach (Quest thisQuest in PopulationContent)
        {
            GameObject next = Instantiate(ScrollContent); //Creates new Scrollview Content
            next.transform.SetParent(ScrollViewTransfrom.transform.parent, false);
            next.SetActive(true);
            QuestButton tryQuest;
            if(next.TryGetComponent(out tryQuest))
            {
                tryQuest.myQuest = thisQuest;
                QuestName = next.GetComponentInChildren<Text>();
                if (QuestName != null)
                {
                    QuestName.text = thisQuest.Name;
                    QuestName.color = Color.black;
                    Debug.Log(QuestName);
                    Debug.Log("Quest added: " + thisQuest.Name);
                }
                else
                    Debug.Log("QuestName is null");

            }
        }
    }

    void SwitchPannel()
    {
        foreach (GameObject page in tabPannels)
        {
            page.SetActive(false);
        }
        ActivePannel.SetActive(true);
    }

    void ResetTabs()
    {
        foreach (QuestButton button in questButtons)
        {
            button.isSelected = false;
        }
        SetColors();
    }

    void SetColors()
    {
        foreach (QuestButton button in questButtons)
        {
            if (button.isSelected)
                button.QuestNameText.color = tabActiveColor;
            else
                button.QuestNameText.color = tabIdleColor;
        }
    }

}
