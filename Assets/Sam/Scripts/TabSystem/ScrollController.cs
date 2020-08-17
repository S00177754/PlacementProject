using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    QuestManager Manager;

    public GameObject ButtonPrefab;

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
        Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");
        FillScrollView();
    }



    public void FillScrollView()
    {
        Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");

        foreach (Quest quest in Manager.FoundMSQuests)
        {
            GameObject next = Instantiate(ButtonPrefab, ScrollviewContent.transform);
            ButtonQuests button;
            TMP_Text TitleText;
            next.TryGetComponent(out button);
            Buttons.Add(button);
            TitleText = next.GetComponentInChildren<TMP_Text>();
            Debug.Log(quest.Name);
            TitleText.text = quest.Name;
            //TitleText.color = buttonIdleColor;
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
