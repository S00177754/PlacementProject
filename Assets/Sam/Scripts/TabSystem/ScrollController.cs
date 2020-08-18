using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
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
        Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");
        FillScrollView();
    }



    public void FillScrollView()
    {
        Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");

        if(tag.Equals("Main Scenario"))
        {
            foreach (Quest quest in Manager.FoundMSQuests)
            {
                GameObject next = Instantiate(ButtonPrefab, ScrollviewContent.transform);
                ButtonQuests button;
                TMP_Text TitleText;
                next.TryGetComponent(out button);
                Buttons.Add(button);
                button.buttonQuest = quest;
                button.Panel = Panel;
                TitleText = next.GetComponentInChildren<TMP_Text>();
                TitleText.text = quest.Name;


                if (button != null)
                    Debug.Log("Button quest is " + button.buttonQuest.name);

            }
        }
        else if(tag.Equals("Side Quest"))
        {
            foreach (Quest quest in Manager.ActiveSides)
            {
                GameObject next = Instantiate(ButtonPrefab, ScrollviewContent.transform);
                ButtonQuests button;
                TMP_Text TitleText;
                next.TryGetComponent(out button);
                Buttons.Add(button);
                button.buttonQuest = quest;
                button.Panel = Panel;
                TitleText = next.GetComponentInChildren<TMP_Text>();
                TitleText.text = quest.Name;


                if (button != null)
                    Debug.Log("Button quest is " + button.buttonQuest.name);

            }
        }
    }

    public void ClearScrollView()
    {
        foreach (ButtonQuests destroyMe in Buttons)
        {
            destroyMe.ClearMe();
            Debug.Log("Button Clear");
        }
    }
}
