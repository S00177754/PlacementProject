﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public enum GameState {MainMenu, Paused, Explore, Pacifist, Driving, GameOver, Chatting}

public class GameStateController : MonoBehaviour
{
    static public GameStateController Instance;

    static public GameState gameState;
    static public GameState previousGameState;

    public PauseMenuController PauseMenu;
    public DialogueManager DialogueBox;
    public GameObject PlayerHUD;


    private void Start()
    {
        DialogueBox = GetComponent<DialogueManager>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        SetGameState(GameState.Explore);
    }

    static public void SetGameState(GameState state)
    {
        previousGameState = gameState;
        gameState = state;

        StateRefresh();  
    }

    static public void ResumePreviousState()
    {
        GameState current = gameState;
        gameState = previousGameState;
        previousGameState = current;
        


        StateRefresh();
    }



    static private void StateRefresh()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                MainMenuRefresh();
                break;

            case GameState.Paused:
                PausedRefresh();               
                break;

            case GameState.Explore:
                ExploreRefresh();
                break;

            case GameState.Pacifist:
                PacifistRefresh();
                break;

            case GameState.Driving:
                DrivingRefresh();
                break;

            case GameState.GameOver:
                GameOverRefresh();
                break;

            case GameState.Chatting:
                ChattingRefresh();
                break;
        }
    }

    static private void MainMenuRefresh()
    {
        Time.timeScale = 1;
        //PlayerController.Instance.GetComponent<PlayerMovement>().SetFreeze(false, false);
        Instance.ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
    }

    static private void PausedRefresh()
    {       
            Time.timeScale = 0;
            Instance.PauseMenu.gameObject.SetActive(true);
            Instance.PauseMenu.GetComponent<PauseMenuController>().PauseGame();
            //PlayerController.Instance.GetComponent<PlayerMovement>().SetFreeze(true, true);
            Instance.ChangeAllPlayerMapsTo("UI");
        
    }

    static void ChattingRefresh()
    {
        Time.timeScale = 0;
        //Instance.DialogueBox.GetComponent<DialogueManager>().gameObject.SetActive(true);
        Instance.DialogueBox.GetComponent<DialogueManager>().InteractButtonPress();
        PlayerController.Instance.GetComponent<PlayerMovement>().SetFreeze(true, true);
        Instance.ChangeAllPlayerMapsTo("UI");
    }

    static private void ExploreRefresh()
    {
        Time.timeScale = 1;
        Instance.PlayerHUD.SetActive(true);
        PauseMenuClear();

        PlayerController.Instance.GetComponent<PlayerMovement>().SetFreeze(false,false);
        Instance.ChangeAllPlayerMapsTo("Player");

    }

    static private void PacifistRefresh()
    {
        Time.timeScale = 1;
        PlayerController.Instance.GetComponent<PlayerMovement>().SetFreeze(false, false);
        Instance.ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
    }

    static private void DrivingRefresh()
    {
        Time.timeScale = 1;
        //PlayerController.Instance.GetComponent<PlayerMovement>().SetFreeze(false, false);
        Instance.ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
    }

    static private void GameOverRefresh()
    {
        Time.timeScale = 1;
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
        //PlayerController.Instance.GetComponent<PlayerMovement>().SetFreeze(true, true);
        Instance.ChangeAllPlayerMapsTo("Player");
    }

    public void ChangeAllPlayerMapsTo(string map)
    {
        //Debug.Log(string.Concat("Map: ", map));
        PlayerController.Instance.GetComponent<PlayerInput>().SwitchCurrentActionMap(map);

    }

    static private void PauseMenuClear()
    {
        if (previousGameState == GameState.Paused)
        {
            Instance.PauseMenu.GetComponent<PauseMenuController>().gameObject.SetActive(false);
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.GetComponent<InputManager>().buttonStates.SetState(EastButtonState.Default);
            }
        }
    }


}
