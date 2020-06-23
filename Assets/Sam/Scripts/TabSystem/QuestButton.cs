using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public QuestSelection questGroup;

    public Text QuestNameText;
    public Canvas MyCanvas;
    public bool isSelected;

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
        questGroup.Subscribe(this);
        isSelected = false;
        MyCanvas.enabled = false;
    }
}
