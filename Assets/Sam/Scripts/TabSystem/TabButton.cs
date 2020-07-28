using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public TMP_Text tabUIText;
    public GameObject MyPannel;
    public bool isSelected;

    public void OnPointerClick(PointerEventData eventData){
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData){
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData){
        tabGroup.OnTabExit(this);
    }
 
    void Start()
    {
        tabUIText = GetComponent<TMP_Text>();
        tabGroup.Subscribe(this);
        isSelected = false;
        MyPannel.SetActive(false);
    }

    void Update()
    {
        
    }
}
