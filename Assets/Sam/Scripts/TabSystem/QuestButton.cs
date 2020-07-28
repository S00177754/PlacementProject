using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public QuestSelection questGroup;
    public Quest myQuest;
    public TMP_Text QuestNameText;
    public bool isSelected;
    public GameObject MyPannel;


    public void OnPointerClick(PointerEventData eventData)
    {
        questGroup.OnQuestSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        questGroup.OnQuestEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        questGroup.OnQuestExit(this);
    }

    void Start()
    {
        //QuestNameText = GetComponent<Text>();
        //questGroup.Subscribe(this);
        //isSelected = false;
        //MyPannel.SetActive(false);
    }

    void OnEnable()
    {
        QuestNameText = GetComponent<TMP_Text>();
        questGroup.Subscribe(this);
        isSelected = false;
        MyPannel.SetActive(false);
    }

}
