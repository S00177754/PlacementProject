using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PauseMenuState { RootMenu, Inventory, Settings, AbilityTree, FastTravel, Quests, Exit}

public class PauseMenuController : MonoBehaviour
{
    static public PauseMenuController Instance;

    public PauseMenuState menuState;
    public PauseMenuState previousState;
    public TMP_Text Clock;

    [Header("Pause Menu State Objects")]
    public SubMenu RootMenu;
    public SubMenu Inventory;
    public SubMenu Settings;
    public SubMenu AbilityTree;
    public SubMenu FastTravel;
    public SubMenu Quests;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        Clock.text = DateTime.Now.ToShortTimeString();
    }

    public void PauseGame()
    {
        SetMenuState(PauseMenuState.RootMenu);
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

    public void QuestButton()
    {
        SetMenuState(PauseMenuState.Quests);
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

            case PauseMenuState.Quests:
                QuestMenuState();
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

        FastTravel.SubMenuObject.GetComponent<FastTravelMenuController>().GenerateList();
    }

    public void QuestMenuState()
    {
        ActivateSingleMenu(PauseMenuState.Quests);
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
                Quests.SubMenuObject.SetActive(false);
                UIHelper.SelectedObjectSet(RootMenu.DefaultSelectedUIElement);
                PlayerController.Instance.GetComponent<InputManager>().SetSelecOnRegain(RootMenu.DefaultSelectedUIElement);
                break;

            case PauseMenuState.Inventory:
                RootMenu.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(true);
                Quests.SubMenuObject.SetActive(false);
                //UIHelper.SelectedObjectSet(Inventory.DefaultSelectedUIElement);
                PlayerController.Instance.GetComponent<InputManager>().SetSelecOnRegain(Inventory.DefaultSelectedUIElement);
                break;

            case PauseMenuState.Settings:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(true);
                Quests.SubMenuObject.SetActive(false);
                UIHelper.SelectedObjectSet(Settings.DefaultSelectedUIElement);
                PlayerController.Instance.GetComponent<InputManager>().SetSelecOnRegain(Settings.DefaultSelectedUIElement);
                break;

            case PauseMenuState.AbilityTree:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(true);
                FastTravel.SubMenuObject.SetActive(false);
                Settings.SubMenuObject.SetActive(false);
                Quests.SubMenuObject.SetActive(false);
                UIHelper.SelectedObjectSet(AbilityTree.DefaultSelectedUIElement);
                PlayerController.Instance.GetComponent<InputManager>().SetSelecOnRegain(AbilityTree.DefaultSelectedUIElement);
                break;

            case PauseMenuState.FastTravel:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(true);
                Settings.SubMenuObject.SetActive(false);
                Quests.SubMenuObject.SetActive(false);
                //UIHelper.SelectedObjectSet(FastTravel.DefaultSelectedUIElement);
                PlayerController.Instance.GetComponent<InputManager>().SetSelecOnRegain(FastTravel.DefaultSelectedUIElement);
                break;

            case PauseMenuState.Quests:
                RootMenu.SubMenuObject.SetActive(false);
                Inventory.SubMenuObject.SetActive(false);
                AbilityTree.SubMenuObject.SetActive(false);
                FastTravel.SubMenuObject.SetActive(false);
                Quests.SubMenuObject.SetActive(true);
                Settings.SubMenuObject.SetActive(Quests.DefaultSelectedUIElement);
                UIHelper.SelectedObjectSet(AbilityTree.DefaultSelectedUIElement);
                PlayerController.Instance.GetComponent<InputManager>().SetSelecOnRegain(Quests.DefaultSelectedUIElement);
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
                SetMenuState(PauseMenuState.RootMenu);
                break;

            case PauseMenuState.FastTravel:
                SetMenuState(PauseMenuState.RootMenu);
                break;

            case PauseMenuState.Quests:
                SetMenuState(PauseMenuState.Quests);
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
