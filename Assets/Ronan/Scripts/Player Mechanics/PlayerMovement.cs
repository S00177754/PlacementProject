using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // ************** Public Variables **************
    [Header("Character States")]
    public bool FreezeMovement = false;
    public bool IsGrounded = true;
    public bool IsCrouching = false;
    public bool IsClimbing = false;
    
    public bool IsJumping = false;
    [HideInInspector]
    public bool IsMoving = false;

    [Header("Movement Speeds")]
    public float WalkSpeed = 5f;
    public float SprintSpeed = 7.5f;
    public float CrouchSpeed = 2f;
    public float ClimbSpeed = 4.5f;
    [HideInInspector]
    public float FallSpeed = -9.81f;

    [Header("Action Variables")]
    public float JumpHeight = 2f;
    public float CrouchHeight = 1f;

    [Header("Camera")]
    public Transform CameraParent;
    public float LookYLimit = 60f;

    [Header("")]
    public Transform PlayerModel;
    public Transform bottomOfPlayer;
    public LayerMask GroundLayer;

    // ************** Private Variables **************
    private PlayerController Controller;

    //Private Variables
    private CharacterController Character;
    private Vector2 input_Move;
    private Vector3 velocity;

    //Used for speed calulations and set with the speed variables above
    private float movementSpeed = 5f;
    private float slideFiction = 0.3f;
    private Vector3 hitNormal;
    private float forwardMultiplier = 1;
    private Vector3 forwardDirection;


    //Camera Rotation 
    private Vector2 rotation = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    // ************** Monobehaviour Methods **************
    void Start()
    {
        Controller = GetComponent<PlayerController>();
        Character = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    void Update()
    {

        ResetVelocity();

        Move(input_Move);

        IsGrounded = TouchingGround();
        
        Gravity();  
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IsGrounded = true;
    }


    // ************** Movement Methods **************

    /// <summary>
    /// Movement calculations for gameobject.
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector2 direction)
    {
        if (!FreezeMovement)
        {
            if (IsCrouching)
                movementSpeed = CrouchSpeed;

            if (direction.sqrMagnitude < 0.01)
                return;

            var scaledMoveSpeed = movementSpeed * Time.deltaTime;

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = scaledMoveSpeed * direction.y;
            float curSpeedY = scaledMoveSpeed * direction.x;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            //Player model rotation so they are facing the correct position
            PlayerModel.rotation = Quaternion.Slerp(PlayerModel.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            
            //Moves position of Character
            Character.Move(moveDirection);
        }
    }

    private void Look(Vector2 direction)
    {
        if (!FreezeMovement)
        {

            rotation.y += direction.x * Controller.Settings.CameraSensitivity;
            if (Controller.Settings.InvertYAxis)
            {
                rotation.x += -direction.y * Controller.Settings.CameraSensitivity;
            }
            else
            {
                rotation.x += direction.y * Controller.Settings.CameraSensitivity;
            }
            rotation.x = Mathf.Clamp(rotation.x, -LookYLimit, LookYLimit);

            CameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }

    private void ResetVelocity()
    {
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Gravity()
    {
        if (velocity.y <= -2)
        {
            IsJumping = false;
        }

        if (!IsGrounded)
        {

            velocity.y += 2f * FallSpeed * Time.deltaTime; // y = ((1/2)*g) * t^2

            Character.Move(velocity * Time.deltaTime); //T = time.deltaTime
        }
    }

    private bool TouchingGround()
    {
        RaycastHit[] hits;

        //Need to refactor but raycasting around the character allows for properground check, by sinking slightly into collider of player, we avoid wall collision issues
        if (RaycastRing(bottomOfPlayer.position, Vector3.down, 0.2f, out hits, 0.15f, GroundLayer))
        {
            return true;
        }

        return false;


    }

    private bool RaycastRing(Vector3 position,Vector3 direction,float radius,out RaycastHit[] hits, float MaxDistance, LayerMask layer)
    {

        hits = new RaycastHit[4];
        if (Physics.Raycast(position + new Vector3(radius, 0.1f, 0), direction, out hits[0], MaxDistance, layer) || //Right
            Physics.Raycast(position + new Vector3(-radius, 0.1f, 0), direction, out hits[1], MaxDistance, layer) || //Left
            Physics.Raycast(position + new Vector3(0, 0.1f, -radius), direction, out hits[2], MaxDistance, layer) || //Front
            Physics.Raycast(position + new Vector3(0, 0.1f, radius), direction, out hits[3], MaxDistance, layer))  //Back
        {
            return true;
        }
        else return false;
    }


    // ************** Input Action Methods **************

    #region Input Actions
    public void OnMove(InputAction.CallbackContext context)
    {
        input_Move = context.ReadValue<Vector2>();

        //When the joystick is no longer active
        if (context.canceled)
        {
            IsMoving = false;
            movementSpeed = WalkSpeed;
        }
        else
        {
            IsMoving = true;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look(context.ReadValue<Vector2>());
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (IsGrounded)
                {
                    velocity.y = Mathf.Sqrt(JumpHeight * -2 * FallSpeed); //v = Square root of (h * -2 * g)
                    Character.Move(velocity * Time.deltaTime);
                    IsGrounded = false;
                    IsJumping = true;
                }
                break;

            default:
                break;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //Sprint Toggle
        if (movementSpeed == WalkSpeed)
        {
            if (IsMoving & !IsCrouching) //To avoid speed increase without moving
            {
                movementSpeed = SprintSpeed; 
            }
        }
        else
        {
            movementSpeed = WalkSpeed;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //Crouch Toggle
        if (IsCrouching)
        {
            IsCrouching = false;
            movementSpeed = WalkSpeed;
        }
        else
        {
            IsCrouching = true;
        }
    }

    #endregion


    // ************** Debug **************
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(bottomOfPlayer.position + new Vector3(0.2f, 0.1f, 0), new Vector3(0,-1,0) * 0.15f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(bottomOfPlayer.position + new Vector3(-0.2f, 0.1f, 0),new Vector3(0,-1,0) * 0.15f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bottomOfPlayer.position + new Vector3(0, 0.1f, -0.2f), new Vector3(0,-1,0) * 0.15f);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(bottomOfPlayer.position + new Vector3(0, 0.1f, 0.2f), new Vector3(0,-1,0) * 0.15f);
    }
}
