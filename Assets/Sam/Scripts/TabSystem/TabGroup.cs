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
        tabIdle = new Color(0,0,0,0);
        tabHover = new Color(0,111,255,0);
        tabActive = new Color(0,255,0,0);
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
        //ResetTabs();
        if(currentlySelected == null || tabButton != currentlySelected)
            tabButton.tabUIText.color = tabHover;
    }

    public void OnTabExit(TabButton tabButton){
        tabButton.tabUIText.color = tabIdle;
        //ResetTabs();
    }

    public void OnTabSelected(TabButton tabButton){
        currentlySelected = tabButton;
        //ResetTabs();
        tabButton.tabUIText.color = tabActive;
    }

    void ResetTabs(){
        foreach(TabButton button in tabButtons){
            if(currentlySelected != null && button == currentlySelected)
            {
                continue;
            }
            button.tabUIText.color = tabIdle;
        }
    }
}
