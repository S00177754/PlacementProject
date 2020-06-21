using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PauseMenuState { RootMenu, Inventory, Settings, AbilityTree, FastTravel, Exit}

public class PauseMenuController : MonoBehaviour
{
    public PauseMenuState menuState;
    public PauseMenuState previousState;
    public TMP_Text Clock;

    [Header("Pause Menu State Objects")]
    public SubMenu RootMenu;
    public SubMenu Inventory;
    public SubMenu Settings;
    public SubMenu AbilityTree;
    public SubMenu FastTravel;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Clock.text = DateTime.Now.ToShortTimeString();
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

    public void AbilityTreeButton()
    {
        SetMenuState(PauseMenuState.AbilityTree);
    }

    public void FastTravelButton()
    {
        SetMenuState(PauseMenuState.FastTravel);
    }

    //Menu State Code
    public void SetMenuState(PauseMenuState state)
    {
        previousState = menuState;
        menuState = state;
        StateRefresh();
    }

    public void StateRefresh()
    {
        if(previousState == PauseMenuState.Inventory)
            Inventory.SubMenuObject.GetComponent<InventorySystemController>().ClearList();

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

            case PauseMenuState.AbilityTree:
                AbilityTreeMenuState();
                break;

            case PauseMenuState.FastTravel:
                FastTravelMenuState();
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

       // Inventory.SubMenuObject.GetComponent<InventoryPanelController>().GenerateList();
        Inventory.SubMenuObject.GetComponent<InventorySystemController>().FilterList();
        Inventory.SubMenuObject.GetComponent<InventorySystemController>().ShowInventoryList();
    }

    private void SettingsMenuState()
    {
        ActivateSingleMenu(PauseMenuState.Settings);
    }

    private void AbilityTreeMenuState()
    {
        ActivateSingleMenu(PauseMenuState.AbilityTree);
    }

    private void FastTravelMenuState()
    {
        ActivateSingleMenu(PauseMenuState.FastTravel);
    }

    public void ActivateSingleMenu(PauseMenuState menu)
    {
        switch (menu)
        {
            case PauseMenuState.RootMenu:
                Inventory.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(false);
                RootMenu.SubMenuObject.SetActive(true);
                UIHelper.SelectedObjectSet(RootMenu.DefaultSelectedUIElement);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().SetSelecOnRegain(RootMenu.DefaultSelectedUIElement);
                break;

            case PauseMenuState.Inventory:
                RootMenu.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(true);
                //UIHelper.SelectedObjectSet(Inventory.DefaultSelectedUIElement);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().SetSelecOnRegain(Inventory.DefaultSelectedUIElement);
                break;

            case PauseMenuState.Settings:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(true);
                UIHelper.SelectedObjectSet(Settings.DefaultSelectedUIElement);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().SetSelecOnRegain(Settings.DefaultSelectedUIElement);
                break;

            case PauseMenuState.AbilityTree:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(true);
                FastTravel.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(false);
                UIHelper.SelectedObjectSet(AbilityTree.DefaultSelectedUIElement);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().SetSelecOnRegain(AbilityTree.DefaultSelectedUIElement);
                break;

            case PauseMenuState.FastTravel:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(true);
                Settings.SubMenuObject.SetActive(false);
                UIHelper.SelectedObjectSet(FastTravel.DefaultSelectedUIElement);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().SetSelecOnRegain(FastTravel.DefaultSelectedUIElement);
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
                GameStateController.ResumePreviousState();
                //Could try calling a seperate event?
                break;

            case PauseMenuState.Inventory:
                SetMenuState(PauseMenuState.RootMenu);
                break;

            case PauseMenuState.Settings:
                SetMenuState(PauseMenuState.RootMenu);
                break;

            case PauseMenuState.AbilityTree:
                SetMenuState(PauseMenuState.AbilityTree);
                break;

            case PauseMenuState.FastTravel:
                SetMenuState(PauseMenuState.FastTravel);
                break;

            default:
                break;
        }
    }
}

[Serializable]
public class SubMenu
{
    public GameObject SubMenuObject;
    public GameObject DefaultSelectedUIElement;
}
