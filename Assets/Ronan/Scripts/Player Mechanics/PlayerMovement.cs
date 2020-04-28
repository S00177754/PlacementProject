using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Character States")]
    public bool FreezeMovement = false;
    public bool IsGrounded = true;
    public bool IsCrouching = false;

    [Header("Movement Speeds")]
    public float WalkSpeed = 5f;
    public float SprintSpeed = 7.5f;
    public float CrouchSpeed = 2f;
    public float ClimbSpeed = 4.5f;
    public float FallSpeed = -9.81f;

    [Header("Action Variables")]
    public float JumpHeight = 2f;
    public float CrouchHeight = 1f;

    [Header("")]
    public Transform bottomOfPlayer;
    public LayerMask GroundLayer;

    //Private Variables
    private CharacterController Character;
    private const float GroundCheckRadius = 0.1f;

    private Vector2 input_Move;
    private Vector3 velocity;
    private bool IsMoving = false;

    //Used for speed calulations and set with the speed variables above
    private float movementSpeed = 5f;
    private float xRotation = 0f;


    void Start()
    {
        Character = GetComponent<CharacterController>();
    }

    void Update()
    {
        IsGrounded = TouchingGround();

        if(IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Move(input_Move);
        Gravity();
    }




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
            var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);

            Character.Move(move * scaledMoveSpeed);
        }
    }



    private void Gravity()
    {
        if (!IsGrounded)
        {
            velocity.y += 2f * FallSpeed * Time.deltaTime; // y = ((1/2)*g) * t^2

            Character.Move(velocity * Time.deltaTime); //T = time.deltaTime
        }
    }

    private void SlideCheck()
    {
        //Raycast Check
        RaycastHit[] hits;
        RaycastRing(bottomOfPlayer.position, Vector3.down, 0.2f, out hits, 0.15f, GroundLayer);

        //Need to check if player centre is above the slope and in range

        //if true then find angle of slope and apply movement force
    }

    private bool TouchingGround()
    {
        RaycastHit[] hits;

        //Need to refactor but raycasting around the character allows for properground check, by sinking slightly into collider of player, we avoid wall collision issues
        if(RaycastRing(bottomOfPlayer.position,Vector3.down,0.2f,out hits,0.15f,GroundLayer))
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
