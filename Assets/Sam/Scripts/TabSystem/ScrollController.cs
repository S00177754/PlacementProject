﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    [SerializeField]
    [Header("Required")]
    QuestManager Manager;

    public GameObject ButtonPrefab;
    public GameObject Panel;
    public GameObject ScrollviewContent;

    public List<ButtonQuests> Buttons;

    [SerializeField]
    private Color buttonIdleColor;
    [SerializeField]
    private Color buttonHoverColor;
    [SerializeField]
    private Color buttonActiveColor;


    void Start()
    {
        Panel.SetActive(false);
        
        FillScrollView();
    }

    //Ensures side panel is only displayed when a button is pressed
    private void OnEnable()
    {
        Panel.SetActive(true);
        FillScrollView();
    }

    private void OnDisable()
    {
        Panel.SetActive(false);
    }

    public void FillScrollView()
    {
        ClearScrollView();

        foreach (Quest quest in Manager.MainScenarioQuests)
        {
            if (quest.isFound)
            {
                GameObject next = Instantiate(ButtonPrefab, ScrollviewContent.transform);


                ButtonQuests button;
                next.TryGetComponent(out button);
                Buttons.Add(button);
                button.buttonQuest = quest;
                button.Panel = Panel;
                
                TMP_Text TitleText;
                TitleText = next.GetComponentInChildren<TMP_Text>();
                TitleText.text = quest.Name;


                if (button != null)
                    Debug.Log("Button quest is " + button.buttonQuest.name);
            }

        }

        //foreach (Quest quest in Manager.FoundMSQuests)
        //{
        //    GameObject next = Instantiate(ButtonPrefab, ScrollviewContent.transform);
        //    ButtonQuests button;
        //    TMP_Text TitleText;
        //    next.TryGetComponent(out button);
        //    Buttons.Add(button);
        //    button.buttonQuest = quest;
        //    button.Panel = Panel;
        //    TitleText = next.GetComponentInChildren<TMP_Text>();
        //    TitleText.text = quest.Name;


        //    if (button != null)
        //        Debug.Log("Button quest is " + button.buttonQuest.name);

        //}

        //if(CompareTag("Main Scenario"))
        //{
        //}
        //else if(CompareTag("Side Quest"))
        //{
        //    foreach (Quest quest in Manager.ActiveSides)
        //    {
        //        GameObject next = Instantiate(ButtonPrefab, ScrollviewContent.transform);
        //        ButtonQuests button;
        //        TMP_Text TitleText;
        //        next.TryGetComponent(out button);
        //        Buttons.Add(button);
        //        button.buttonQuest = quest;
        //        button.Panel = Panel;
        //        TitleText = next.GetComponentInChildren<TMP_Text>();
        //        TitleText.text = quest.Name;


        //        if (button != null)
        //            Debug.Log("Button quest is " + button.buttonQuest.name);

        //    }
        //}
    }

    public void ClearScrollView()
    {
        foreach (ButtonQuests destroyMe in Buttons)
        {
            destroyMe.ClearMe();
            Debug.Log("Button Clear");
        }
        Buttons.Clear();
    }
}
