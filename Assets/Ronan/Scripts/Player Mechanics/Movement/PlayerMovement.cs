using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region Public Variables 

    [Header("Character States")]
    public bool FreezeMovement = false; //Freezes movement of player input
    public bool IsGrounded = true; 
    public bool IsCrouching = false; 
    public bool IsJumping = false;
    public bool IsFalling = false; 
    public bool IsRolling = false; //Shows if player is using dodge roll
    [HideInInspector]
    public bool IsMoving = false; //Shows if player is currently moving from input


    [Header("Movement Speeds")]
    public float WalkSpeed = 5f; 
    public float SprintSpeed = 7.5f; 
    public float CrouchSpeed = 2f; 
    [HideInInspector]
    public float ClimbSpeed = 4.5f; //Speed for climbing, not yet implimented
    [HideInInspector]
    public float FallSpeed = -9.81f; //Fall speed for gravity calculations


    [Header("Action Variables")]
    public float JumpHeight = 2f; //Height which the player can jump by


    [Header("Camera")]
    public Transform CameraParent; //Transform of the object which the camera is a parent of, used for rotation of camera around player
    public float LookYLimit = 60f; //Vertical look limits for player camera to move
    public bool FreezeCamera = false; //Boolean to control the freezing of camera rotation, used mainly for pause screen
    public bool IsCameraFocusedOn = false;
    public Transform FocusPosition;

    [Header("Model Refrences")]
    public PlayerAnimator Animator; //Access to animator component on child object so we can trigger relevant animations or adjust animation variables
    public Transform PlayerModel; //Player model transform for movement, this means we can change out the model for the player easily

    #endregion


    #region Private Variables 

    private PlayerController Controller; //Holds reference  to player controller for acces to settings and health info
    private CharacterController Character; //Character Controller reference for applying movement of player
    private PlayerSurroundingDetection Detect; //Reference to detecting script which has OnGround check implimented

    private Vector2 input_Move; //Direction for movement provided from the input manager
    private Vector2 input_Look; //Direction for movement provided from the input manager
    private Vector3 velocity; //Used for velocity and gravity calculations, also used for setting of is jumping and is falling booleans

    private float movementSpeed = 5f; //Used for speed calulations and set with the speed variables above


    //Camera Rotation 
    private Vector2 rotation = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    #endregion


    #region Monobehaviour Methods 
    void Start()
    {
        Controller = GetComponent<PlayerController>();
        Character = GetComponent<CharacterController>();
        Detect = GetComponent<PlayerSurroundingDetection>();

        rotation.y = transform.eulerAngles.y;
        movementSpeed = WalkSpeed;
    }

    void Update()
    {
        if (FreezeMovement)
        {
            input_Move = Vector2.zero;
        }

        IsGrounded = Detect.OnGround();

        if(IsFalling && Detect.LandOnGroundCheck())
        {
            Animator.SetTrigger("FallToLand");
        }
        if(!IsGrounded && velocity.y > 0)
        {
            IsJumping = true;
            IsFalling = false;
        }
        else if (!IsGrounded && velocity.y < 0)
        {
            IsFalling = true;
            IsJumping = false;
        }
        else if (IsGrounded)
        {
            IsFalling = false;
            IsJumping = false;
        }



        ResetVelocity();

        Move(input_Move);
        if (IsCameraFocusedOn)
        {
            Quaternion modelRotation = PlayerModel.rotation; //Rotation of model
            //transform.LookAt(FocusPosition.transform);
            var lookPos = FocusPosition.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 360);
            PlayerModel.rotation = modelRotation;
            input_Look = Vector2.zero;
        }
        else
        {
            Look(input_Look);
        }
        Gravity();
    }

    #endregion


    #region Movement 

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

            if (direction.sqrMagnitude < 0.01) //If character not moving then don't bother carrying out calculations
            {
                if (Animator != null && IsGrounded)
                {
                    Animator.SetSpeed(0);
                }
                if (IsRolling)
                {
                    RollMovement();
                }
                return;
            }

            if (IsRolling)
            {
                RollMovement();
                return;
            }

            //Scale speed so framerate doesn't affect it
            var scaledMoveSpeed = movementSpeed * Time.deltaTime;


            //Calculations for moving the player relative to camera position
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = scaledMoveSpeed * direction.y;
            float curSpeedY = scaledMoveSpeed * direction.x;

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            //Player model rotation so they are facing the correct position
            PlayerModel.rotation = Quaternion.Slerp(PlayerModel.rotation, Quaternion.LookRotation(moveDirection), 0.15F);

            //Setting of speed animation variable for animations blend trees
            if (Animator != null && IsGrounded)
            {

                if (movementSpeed == WalkSpeed)
                {
                    Animator.SetSpeed(0.5f);
                }

                if (movementSpeed == SprintSpeed)
                {
                    Animator.SetSpeed(1);
                }

                if (movementSpeed == CrouchSpeed)
                {
                    if (moveDirection.magnitude <= 0.015)
                    {
                        Animator.SetSpeed(0.25f);
                    }
                    else
                    {
                        Animator.SetSpeed(0.5f);
                    }

                }

            }

            //Moves position of Character
            Character.Move(moveDirection);
        }
    }


    /// <summary>
    /// Logic for view direction and camera movement/rotation
    /// </summary>
    /// <param name="direction"></param>
    private void Look(Vector2 direction)
    {
        if (!FreezeCamera)
        {

            rotation.y += direction.x * Controller.Settings.CameraSensitivity; //Camera Sensitivity

            if (Controller.Settings.InvertYAxis)
            {
                rotation.x += -direction.y * Controller.Settings.CameraSensitivity;
            }
            else
            {
                rotation.x += direction.y * Controller.Settings.CameraSensitivity;
            }

            rotation.x = Mathf.Clamp(rotation.x, -LookYLimit, LookYLimit); //Clamps vertical view

            Quaternion modelRotation = PlayerModel.rotation; //Rotation of model
            CameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0); //Moves camera around the player seperatly
            transform.eulerAngles = new Vector2(0, rotation.y);
            PlayerModel.rotation = modelRotation;
        }
    }

    public void DodgeRoll()
    {
        if (IsGrounded && !GetComponent<PlayerAttack>().IsAttacking && !GetComponent<PlayerAttack>().IsCharging)
        {
            movementSpeed = WalkSpeed;
            IsRolling = true;
            IsMoving = true;
            GetComponent<PlayerController>().IsInvincible = true;
            Animator.SetTrigger("DodgeRoll");
            StartCoroutine(DodgeIFrames(1.5f));
        }

    }

    public void RollMovement()
    {
        var scaledMoveSpeed = 4f * Time.deltaTime;

        //Calculations for moving the player relative to camera position
        Vector3 forward = GetComponent<PlayerAnimator>().Animator.gameObject.transform.TransformDirection(Vector3.forward);
        Vector3 right = GetComponent<PlayerAnimator>().Animator.gameObject.transform.TransformDirection(Vector3.right);

        float curSpeedX = scaledMoveSpeed * 1;
        float curSpeedY = scaledMoveSpeed * 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //Player model rotation so they are facing the correct position
        PlayerModel.rotation = Quaternion.Slerp(PlayerModel.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
        if (IsGrounded)
            Character.Move(moveDirection);
    }

    #endregion


    private void ResetVelocity()
    {
        if (IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Gravity()
    {
       

            velocity.y += 2f * FallSpeed * Time.deltaTime; // y = ((1/2)*g) * t^2

            Character.Move(velocity * Time.deltaTime); //T = time.deltaTime
        
    }

    public void SetFreeze(bool movement, bool camera)
    {
        FreezeMovement = movement;
        FreezeCamera = camera;
    }
    public void SetFreezeCam(bool camera)
    {
        FreezeCamera = camera;
    }



    public void LockOntoObject(Transform transform)
    {
        IsCameraFocusedOn = true;
        FocusPosition = transform;
        SetFreezeCam(true);
    }

    public void UnlockFromObject()
    {
        IsCameraFocusedOn = false;
        FocusPosition = null;
        SetFreezeCam(false);
    }

    // ************** Input Action Methods **************

    #region Input Actions Helpers

    public void IsMovingCancelled()
    {
            IsMoving = false;
        movementSpeed = WalkSpeed;
    }

    public void IsMovingActive()
    {
        if(!FreezeMovement)
        IsMoving = true;
    }


    public void InputLook(Vector2 direction)
    {
        
        if (FreezeCamera)
        {
            input_Look = Vector2.zero;
            return;
        }
        else
        {
            input_Look = direction;
        }

    }

    public void InputMove(Vector2 direction)
    {
            input_Move = direction;
    }

    public void Jump()
    {
        if (IsGrounded && !FreezeMovement)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2 * FallSpeed); //v = Square root of (h * -2 * g)
            Character.Move(velocity * Time.deltaTime);
        }
    }

    public void SprintActivate()
    {
        if (!FreezeMovement)
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
    }

    public void CrouchToggle()
    {
        if (!FreezeMovement)
        {
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
    }
    #endregion


    // Coroutines
    public IEnumerator DodgeIFrames(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<PlayerController>().IsInvincible = false;
        Animator.Animator.ResetTrigger("DodgeRoll");
        IsRolling = false;
        IsMoving = false;
    }

}
