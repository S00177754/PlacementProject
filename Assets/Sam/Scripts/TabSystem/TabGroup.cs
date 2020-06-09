using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton currentlySelected; 
    public List<Canvas> tabCanveses;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;


    void Start(){
        tabIdle = new Color(0,0,0,1);
        tabHover = new Color(0,111,255,1);
        tabActive = new Color(0,255,0,1);
    }

    public void Subscribe(TabButton tabButton){
        if(tabButtons == null){
            tabButtons = new List<TabButton>();
            tabCanveses = new List<Canvas>();
        }

        tabButtons.Add(tabButton);
        tabCanveses.Add(tabButton.MyCanvas);

    }

    public void OnTabEnter(TabButton tabButton){
        ResetTabs();
        if(!tabButton.isSelected)
            tabButton.tabUIText.color = tabHover;
    }

    public void OnTabExit(TabButton tabButton){
        tabButton.tabUIText.color = tabIdle;
        //ResetTabs();
    }

    public void OnTabSelected(TabButton tabButton){
        foreach (TabButton button in tabButtons)
        {
            button.isSelected = !button.isSelected;
        }
        ResetTabs();
        tabButton.tabUIText.color = tabActive;
    }

    void ResetTabs(){
        foreach(TabButton button in tabButtons){
            if(button.isSelected)
            {
                continue;
            }
            button.tabUIText.color = tabIdle;
        }
    }
}
