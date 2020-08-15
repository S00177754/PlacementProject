using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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

    public RadialMenuController ActiveRadialMenu;
    public enum RadialMenuState {None, ItemWheel, ItemSetter}
    public RadialMenuState radialMenuState;

    private InventorySystemController Controller;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        buttonStates = new ButtonStates();
        radialMenuState = RadialMenuState.None;
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

    public void SetRadialMenu(RadialMenuController activeRadial, RadialMenuState radialState)
    {
        ActiveRadialMenu = activeRadial;
        radialMenuState = radialState;
    }

    public void CloseRadialMenu()
    {
        if(ActiveRadialMenu != null)
        ActiveRadialMenu.gameObject.SetActive(false);
        SetRadialMenu(null, RadialMenuState.None);
    }

    public void SetInventorySystem(InventorySystemController controller)
    {
        Controller = controller;
    }



    //PLAYER 
    public void OnMove(InputAction.CallbackContext context)
    {
        switch(buttonStates.LeftJoystickState)
        {
            case LeftJoystickState.Default:
            GetComponent<PlayerMovement>().InputMove(context.ReadValue<Vector2>());
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                case InputActionPhase.Started:
                    GetComponent<PlayerMovement>().IsMovingActive();
                    break;

                case InputActionPhase.Canceled:
                        GetComponent<PlayerMovement>().IsMovingCancelled(); 
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        switch(buttonStates.RightJoystickState)
        {
            case RightJoystickState.Default:
            GetComponent<PlayerMovement>().InputLook(context.ReadValue<Vector2>());
                break;

            case RightJoystickState.RadialMenu:
            ActiveRadialMenu.Input(context.ReadValue<Vector2>());
            GetComponent<PlayerMovement>().InputLook(Vector2.zero);
                break;

            default:
                break;
        }

    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        switch(buttonStates.EastBtnState)
        {
            case EastButtonState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if ( context.interaction is TapInteraction)
                    {

                        GetComponent<PlayerAttack>().Attack();
                    }
                    else if(context.interaction is SlowTapInteraction)
                    {
                        GetComponent<PlayerAttack>().ChargeAttack((float)context.duration);
                        
                    }
                    GetComponent<PlayerAttack>().IsCharging = false;
                    break;

                case InputActionPhase.Started:
                    if (context.interaction is SlowTapInteraction)
                    {
                        GetComponent<PlayerAttack>().Charge();
                       
                    }
                    break;

                case InputActionPhase.Canceled:
                        GetComponent<PlayerAttack>().CancelCharge();
                        break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch(buttonStates.SouthBtnState)
        {
            case SouthButtonState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    GetComponent<PlayerMovement>().Jump();
                    break;

                case InputActionPhase.Started:
                    break;

                case InputActionPhase.Canceled:
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }       
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        switch(buttonStates.LeftJoystickState)
        {
            case LeftJoystickState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    GetComponent<PlayerMovement>().SprintActivate();
                    break;

                case InputActionPhase.Started:
                    break;

                case InputActionPhase.Canceled:
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }

    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        switch(buttonStates.RightJoystickState)
        {
            case RightJoystickState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    GetComponent<PlayerMovement>().CrouchToggle();
                    break;

                case InputActionPhase.Started:
                    break;

                case InputActionPhase.Canceled:
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Performed:
            GameStateController.SetGameState(GameState.Paused);
                break;

            default:
                break;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        switch(buttonStates.WestBtnState) //Change To Switch, more efficient
        {
            case WestButtonState.PickupItem:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (context.interaction is SlowTapInteraction || context.interaction is TapInteraction)
                    {
                        GetComponent<InventoryManager>().PickUpItem();
                    }
                    break;

                case InputActionPhase.Started:
                case InputActionPhase.Canceled:
                default:
                    break;
            }
                break;

            case WestButtonState.Merchant:
                switch (context.phase)
                {
                    case InputActionPhase.Performed:
                        Debug.Log("Merchant activated");
                        MerchantUIController.Instance.ShowRootMenu();
                        SwitchToMap("UI");
                        break;

                    case InputActionPhase.Started:
                    case InputActionPhase.Canceled:
                    default:
                        break;
                }
                break;

            case WestButtonState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (context.interaction is TapInteraction)
                    {
                        GetComponent<PlayerMovement>().DodgeRoll();
                    }
                    break;

                case InputActionPhase.Started:
                    
                    break;

                case InputActionPhase.Canceled:
                default:
                    break;
            }
                break;

            case WestButtonState.TravelPoint:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (context.interaction is SlowTapInteraction || context.interaction is TapInteraction)
                    {
                        Debug.Log("Activate teleport Stone");
                    }
                    break;

                case InputActionPhase.Started:
                case InputActionPhase.Canceled:
                default:
                    break;
            }
                break;

            default:
                break;
        }
    }

    public void OnLeftShoulder(InputAction.CallbackContext context)
    {
        switch(buttonStates.LeftShdState)
        {
            case LeftShoulderState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    break;

                case InputActionPhase.Started:
                    GetComponent<TargetManager>().LockOnTarget();
                    break;

                case InputActionPhase.Canceled:
                    GetComponent<TargetManager>().UnlockOnTarget();
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }
    }

    public void OnRightShoulder(InputAction.CallbackContext context)
    {
        switch(buttonStates.RightShdState)
        {
            case RightShoulderState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    break;

                case InputActionPhase.Started:
                    if (radialMenuState == RadialMenuState.None)
                    {
                        SetRadialMenu(GetComponent<PlayerController>().HUDController.ActivateRadialMenu(), RadialMenuState.ItemWheel);
                        buttonStates.RightJoystickState = RightJoystickState.RadialMenu;
                    }
                    break;

                case InputActionPhase.Canceled:
                    if (radialMenuState == RadialMenuState.ItemWheel)
                    {
                        GetComponent<PlayerController>().HUDController.CloseRadialMenu();
                        buttonStates.RightJoystickState = RightJoystickState.Default;
                        SetRadialMenu(null, RadialMenuState.None);
                    }
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
            break;
        }
    }

    public void OnRightTrigger(InputAction.CallbackContext context)
    {
        switch(buttonStates.RightTrgState)
        {
            case RightTriggerState.Default:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    break;

                case InputActionPhase.Started:
                    break;

                case InputActionPhase.Canceled:
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }
    }

    //UI

    public void OnResume(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Performed:
            GameStateController.ResumePreviousState();
                break;

            default:
                break;
            
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        switch (buttonStates.EastBtnState)
        {

            case EastButtonState.Default:
                switch (context.phase)
                {
                    case InputActionPhase.Performed:
                        GetComponent<PlayerController>().PauseMenu.PreviousMenu();
                        break;

                    default:
                        break;
                }
                break;

            case EastButtonState.ItemUsagePanel:
                switch (context.phase)
                {
                    case InputActionPhase.Performed:
                        InventoryItemUsagePanel.Instance.CloseUsagePanel();
                        break;

                    default:
                        break;
                }
                break;

            default:
                break;
        }

    }

    public void OnRightJoystickUI(InputAction.CallbackContext context)
    {
        switch(buttonStates.RightJoystickState)
        {
            case RightJoystickState.RadialMenu:
            switch(radialMenuState)
            {
                case RadialMenuState.ItemSetter:
                (ActiveRadialMenu as ItemSettingRadialMenu).Input(context.ReadValue<Vector2>());
                    break;

                default:
                    break;
            }
            GetComponent<PlayerMovement>().InputLook(Vector2.zero);
                break;

            default:
                break;
        }

    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        switch(buttonStates.SouthBtnState)
        {
            case SouthButtonState.RadialMenu:
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (radialMenuState != RadialMenuState.ItemWheel)
                    {
                        (ActiveRadialMenu as ItemSettingRadialMenu).ActivatedBy.CloseItemRadialSetter();
                        buttonStates.SetState(SouthButtonState.Default);
                        radialMenuState = RadialMenuState.None;
                    }
                    break;

                case InputActionPhase.Started:
                    break;

                case InputActionPhase.Canceled:
                    break;

                default:
                    print(context.phase);
                    break;
            }
                break;

            default:
                break;
        }
    }

}

//Right Hand Side X,A,B,Y
public enum NorthButtonState { Default }
public enum EastButtonState { Default, ItemUsagePanel }
public enum SouthButtonState { Default, RadialMenu }
public enum WestButtonState { Default, PickupItem, TravelPoint, Merchant }


public enum LeftShoulderState { Default }
public enum RightShoulderState { Default }

public enum LeftTriggerState { Default }
public enum RightTriggerState { Default }

public enum LeftJoystickState { Default }
public enum RightJoystickState { Default, RadialMenu }

public enum DPadUp { Default }
public enum DPadDown { Default }
public enum DPadLeft { Default}
public enum DPadRight { Default}

public class ButtonStates
{
    public NorthButtonState NorthBtnState;
    public EastButtonState EastBtnState;
    public SouthButtonState SouthBtnState;
    public WestButtonState WestBtnState;

    public LeftShoulderState LeftShdState;
    public RightShoulderState RightShdState;
    public LeftTriggerState LeftTrgState;
    public RightTriggerState RightTrgState;

    public LeftJoystickState LeftJoystickState;
    public RightJoystickState RightJoystickState;

    public DPadUp UpDpadState;
    public DPadDown DownDpadState;
    public DPadLeft LeftDpadState;
    public DPadRight RightDpadState;

    public ButtonStates()
    {
        NorthBtnState = NorthButtonState.Default;
        EastBtnState = EastButtonState.Default;
        SouthBtnState = SouthButtonState.Default;
        WestBtnState = WestButtonState.Default;
        RightTrgState = RightTriggerState.Default;
        LeftTrgState = LeftTriggerState.Default;
        RightShdState = RightShoulderState.Default;
        LeftShdState = LeftShoulderState.Default;
        LeftJoystickState = LeftJoystickState.Default;
        RightJoystickState = RightJoystickState.Default;

        UpDpadState = DPadUp.Default;
        DownDpadState = DPadDown.Default;
        LeftDpadState = DPadLeft.Default;
        RightDpadState = DPadRight.Default;
    }

    #region SetState Method
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

    public void SetState(RightShoulderState state)
    {
        RightShdState = state;
    }
    public void SetState(LeftShoulderState state)
    {
        LeftShdState = state;
    }
    public void SetState(RightTriggerState state)
    {
        RightTrgState = state;
    }
    public void SetState(LeftTriggerState state)
    {
        LeftTrgState = state;
    }
    public void SetState(LeftJoystickState state)
    {
        LeftJoystickState = state;
    }
    public void SetState(RightJoystickState state)
    {
        RightJoystickState = state;
    }

    public void SetState(DPadUp state)
    {
        UpDpadState = state;
    }
    public void SetState(DPadDown state)
    {
        DownDpadState = state;
    }
    public void SetState(DPadLeft state)
    {
        LeftDpadState = state;
    }
    public void SetState(DPadRight state)
    {
        RightDpadState = state;
    }

    #endregion

}
