﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PauseMenuState { RootMenu, Inventory, Settings, Exit}

public class PauseMenuController : MonoBehaviour
{
    public PauseMenuState menuState;

    [Header("Pause Menu State Objects")]
    public PauseSubMenu RootMenu;
    public PauseSubMenu Inventory;
    public PauseSubMenu Settings;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        SetMenuState(PauseMenuState.RootMenu);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    //Buttons
    public void ResumeGameButton()
    {
        GameStateController.ResumePreviousState();
    }

    public void InventoryButton()
    {
        SetMenuState(PauseMenuState.Inventory);
    }

    public void SettingsButton()
    {
        SetMenuState(PauseMenuState.Settings);
    }

    //Menu State Code
    public void SetMenuState(PauseMenuState state)
    {
        menuState = state;

        StateRefresh();
    }

    public void StateRefresh()
    {
        switch (menuState)
        {
            case PauseMenuState.RootMenu:
                RootMenuSetup();
                break;

            case PauseMenuState.Inventory:
                InventoryMenuSetup();
                break;

            case PauseMenuState.Settings:
                SettingsMenuState();
                break;

            case PauseMenuState.Exit:
                GameStateController.ResumePreviousState();
                break;
        }
    }

    private void RootMenuSetup()
    {
        ActivateSingleMenu(PauseMenuState.RootMenu);
    }

    private void InventoryMenuSetup()
    {
        ActivateSingleMenu(PauseMenuState.Inventory);

        //Use event system to set first obj for controller action
    }

    private void SettingsMenuState()
    {
        ActivateSingleMenu(PauseMenuState.Settings);
    }

    public void ActivateSingleMenu(PauseMenuState menu)
    {
        switch (menu)
        {
            case PauseMenuState.RootMenu:
                RootMenu.SubMenuObject.SetActive(true);
                Inventory.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(false);
                UIHelper.SelectedObjectSet(RootMenu.DefaultSelectedUIElement);
                break;

            case PauseMenuState.Inventory:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(true);
                Settings.SubMenuObject.SetActive(false);
                UIHelper.SelectedObjectSet(Inventory.DefaultSelectedUIElement);
                break;

            case PauseMenuState.Settings:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(true);
                UIHelper.SelectedObjectSet(Settings.DefaultSelectedUIElement);
                break;
        }
    }

    public void PreviousMenu()
    {
        if (!gameObject.activeSelf)
            return;

        switch (menuState)
        {
            case PauseMenuState.RootMenu:
                //Throwing issue for some strange reason? Button press to state change too long?
                //GameStateController.ResumePreviousState();
                //Could try calling a seperate event?
                break;

            case PauseMenuState.Inventory:
                SetMenuState(PauseMenuState.RootMenu);
                break;

            case PauseMenuState.Settings:
                SetMenuState(PauseMenuState.RootMenu);
                break;

            default:
                break;
        }
    }
}

[Serializable]
public class PauseSubMenu
{
    public GameObject SubMenuObject;
    public GameObject DefaultSelectedUIElement;
}
