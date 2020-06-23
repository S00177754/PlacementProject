using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestSelection : MonoBehaviour
{
    public List<QuestButton> questButtons;
    public TabButton currentlySelected;
    public List<Canvas> tabCanveses;
    public Canvas activePage;
    public Color tabIdleColor;
    public Color tabHoverColor;
    public Color tabActiveColor;

    public void OnQuestSelected(QuestButton questButton)
    {
        //Switch active panel to show current quest details

        throw new System.NotImplementedException();
    }

    public void OnQuestEnter(QuestButton questButton)
    {
        throw new System.NotImplementedException();
    }

    public void OnQuestExit(QuestButton questButton)
    {
        throw new System.NotImplementedException();
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

    // Start is called before the first frame update
    void Start()
    {
        //Load cuttent Quest list
        QuestManager Manager = Resources.Load<QuestManager>("Sam/ScriptableObjects/QuestManager");

        //Manager.

        tabIdleColor = new Color(0, 0, 0, 1);
        tabHoverColor = new Color(0, 111, 255, 1);
        tabActiveColor = new Color(0, 255, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
