using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private GameStateController gameStateController;
    private PlayerInput playerInput;
    public ButtonStates buttonStates;
    public GameObject SelectedOnRegained;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        buttonStates = new ButtonStates();
        gameStateController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>();
    }



    public void SwitchToMap(string map)
    {
        playerInput.SwitchCurrentActionMap(map);
    }

    public void SetSelecOnRegain(GameObject go)
    {
        SelectedOnRegained = go;
    }

    public void OnRegain()
    {
        UIHelper.SelectedObjectSet(SelectedOnRegained);
    }

    //PLAYER 

        //Refactor for button states! Will need a state for each action however

    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if(buttonStates.EastBtnState == EastButtonState.Default)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (context.interaction is SlowTapInteraction || context.interaction is TapInteraction)
                    {
                        GetComponent<PlayerAttack>().Attack();
                    }
                    break;

                case InputActionPhase.Started:
                    if (context.interaction is SlowTapInteraction)
                    {
                        GetComponent<PlayerAttack>().Charge();
                    }
                    break;

                case InputActionPhase.Canceled:
                    GetComponent<PlayerAttack>().IsCharging = false;
                    break;

                default:
                    print(context.phase);
                    break;
            }
        }
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if(buttonStates.WestBtnState == WestButtonState.PickupItem)
            {
                //Execute collection code
                GetComponent<InventoryManager>().PickUpItem();
            }
            else if(buttonStates.WestBtnState == WestButtonState.Default)
            {
                if (!GetComponent<PlayerAttack>().WeaponSheathed)
                    GetComponent<PlayerAttack>().WeaponSheathed = true;
            }

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
            GetComponent<PlayerController>().PauseMenu.PreviousMenu();
            
        }

    }

}


public enum NorthButtonState { Default }
public enum EastButtonState { Default }
public enum SouthButtonState { Default }
public enum WestButtonState { Default, PickupItem }

public class ButtonStates
{
    public NorthButtonState NorthBtnState;
    public EastButtonState EastBtnState;
    public SouthButtonState SouthBtnState;
    public WestButtonState WestBtnState;

    public ButtonStates()
    {
        NorthBtnState = NorthButtonState.Default;
        EastBtnState = EastButtonState.Default;
        SouthBtnState = SouthButtonState.Default;
        WestBtnState = WestButtonState.Default;
    }

    public void SetState(NorthButtonState state)
    {
        NorthBtnState = state;
    }

    public void SetState(EastButtonState state)
    {
        EastBtnState = state;
    }

    public void SetState(SouthButtonState state)
    {
        SouthBtnState = state;
    }

    public void SetState(WestButtonState state)
    {
        WestBtnState = state;
    }
}
