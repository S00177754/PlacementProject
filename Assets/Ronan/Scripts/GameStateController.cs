using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public enum GameState {MainMenu, Paused, Explore, Pacifist, Driving, GameOver}

public class GameStateController : MonoBehaviour
{
    static public GameStateController Instance;

    static public GameState gameState;
    static public GameState previousGameState;

    public PauseMenuController PauseMenu;
    public GameObject PlayerHUD;


    private void Start()
    {
        Instance = this;
        SetGameState(GameState.Explore);
    }

    private void Update()
    {
       
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
        }
    }

    static private void MainMenuRefresh()
    {
        Time.timeScale = 1;
        ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
    }

    static private void PausedRefresh()
    {
        Time.timeScale = 0;
        Instance.PauseMenu.GetComponent<PauseMenuController>().PauseGame();
        ChangeAllPlayerMapsTo("UI");
    }

    static private void ExploreRefresh()
    {
        Time.timeScale = 1;
        ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(true);
        PauseMenuClear();
    }

    static private void PacifistRefresh()
    {
        Time.timeScale = 1;
        ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
    }

    static private void DrivingRefresh()
    {
        Time.timeScale = 1;
        ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
    }

    static private void GameOverRefresh()
    {
        Time.timeScale = 1;
        ChangeAllPlayerMapsTo("Player");
        Instance.PlayerHUD.SetActive(false);
        PauseMenuClear();
    }

    static private void ChangeAllPlayerMapsTo(string map)
    {
        Instance.GetComponent<GameManager>().Players.ForEach(p => p.GetComponent<PlayerInput>().SwitchCurrentActionMap(map));

    }

    static private void PauseMenuClear()
    {
        if (previousGameState == GameState.Paused)
        {
            Instance.PauseMenu.GetComponent<PauseMenuController>().HideMenu();
        }
    }
}
