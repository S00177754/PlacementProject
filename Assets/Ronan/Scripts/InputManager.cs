using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private GameStateController gameStateController;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
    }

    public void SwitchToMap(string map)
    {
        playerInput.SwitchCurrentActionMap(map);
    }

    //PLAYER 

    public void OnMove(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {

    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }

    public void OnSprint(InputAction.CallbackContext context)
    {

    }

    public void OnCrouch(InputAction.CallbackContext context)
    {

    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameStateController.SetGameState(GameState.Paused);
        }
    }

    //UI

    public void OnResume(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            GameStateController.ResumePreviousState();
            
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameStateController.Instance.PauseMenu.PreviousMenu();

        }
    }

}
