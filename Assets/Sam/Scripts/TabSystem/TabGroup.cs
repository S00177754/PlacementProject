using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public void Subscribe(TabButton tabButton){
        if(tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(tabButton);

        // tabIdle = new Color(1,1,1,0.5f);
        // tabHover = new Color(0.5f,0.5f,0.5f,0.75f);
        // tabActive = new Color(0.9f, 0.2f,0.2f,0.75f);
    }

    public void OnTabEnter(TabButton tabButton){
        tabButton.tabUIText.color = tabHover;
    }

    public void OnTabExit(TabButton tabButton){
        tabButton.tabUIText.color = tabIdle;
    }

    public void OnTabSelected(TabButton tabButton){
        if(!tabButton.tabSelected){
            //if tab is not selected, chage to active color and switch to is active
            tabButton.tabUIText.color = tabActive;
            tabButton.tabSelected = !tabButton.tabSelected;
        }
        else{
            //If tab is active, change to hover color
            tabButton.tabUIText.color = tabHover;
            tabButton.tabSelected = !tabButton.tabSelected;
        }
    }
}
