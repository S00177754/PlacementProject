﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton currentlySelected; 
    public List<Canvas> tabCanveses;
    public Canvas activePage;
    public Color tabIdleColor;
    public Color tabHoverColor;
    public Color tabActiveColor;


    void Start(){
        tabIdleColor = new Color(0,0,0,1);
        tabHoverColor = new Color(0,111,255,1);
        tabActiveColor = new Color(0,255,0,1);
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
        if (tabButton.isSelected)
            tabButton.tabUIText.color = tabActiveColor;
        else
            tabButton.tabUIText.color = tabHoverColor;

    }

    public void OnTabExit(TabButton tabButton){
        if (tabButton.isSelected)
            tabButton.tabUIText.color = tabActiveColor;
        else
            tabButton.tabUIText.color = tabIdleColor;
    }

    public void OnTabSelected(TabButton tButton){
        ResetTabs();
        tButton.isSelected = !tButton.isSelected;
        activePage = tButton.MyCanvas;

        foreach (TabButton button in tabButtons)
        {
            if (button.isSelected)
                button.tabUIText.color = tabActiveColor;
        }
        SwitchCanvas();

    }

    void ResetTabs(){
        foreach (TabButton button in tabButtons)
        {
            button.isSelected = false;
        }
        SetColors();

    }

    void SetColors()
    {
        foreach (TabButton button in tabButtons)
        {
            if (button.isSelected)
                button.tabUIText.color = tabActiveColor;
            else
                button.tabUIText.color = tabIdleColor;
        }
    }

    void SwitchCanvas()
    {
        foreach (Canvas page in tabCanveses)
        {
            page.enabled = false;
        }
        activePage.enabled = true;
    }
}