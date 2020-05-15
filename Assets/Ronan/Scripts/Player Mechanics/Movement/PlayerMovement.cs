﻿using System;
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
    public bool FreezeCamera = false;
    public bool IsGrounded = true;
    public bool IsCrouching = false;
    public bool IsClimbing = false;
    
    public bool IsJumping = false;
    public bool IsFalling = false;
    public bool IsRolling = false;
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
    public PlayerAnimator Animator;
    public Transform PlayerModel;
    public Transform bottomOfPlayer;
    //public List<LayerMask> Layers;

    // ************** Private Variables **************
    private PlayerController Controller;

    //Private Variables
    private CharacterController Character;
    private PlayerSurroundingDetection Detect;
    private Vector2 input_Move;
    private Vector3 velocity;

    //Used for speed calulations and set with the speed variables above
    private float movementSpeed = 5f;


    //Camera Rotation 
    private Vector2 rotation = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    // ************** Monobehaviour Methods **************
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
        Gravity();
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

    private void Look(Vector2 direction)
    {
        if (!FreezeCamera)
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

            Quaternion modelRotation = PlayerModel.rotation;
            CameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0); //Moves camera around the player seperatly
            transform.eulerAngles = new Vector2(0, rotation.y);
            PlayerModel.rotation = modelRotation;
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
       

            velocity.y += 2f * FallSpeed * Time.deltaTime; // y = ((1/2)*g) * t^2

            Character.Move(velocity * Time.deltaTime); //T = time.deltaTime
        
    }

    public void SetFreeze(bool movement, bool camera)
    {
        FreezeMovement = movement;
        FreezeCamera = camera;
    }

    public void DodgeRoll()
    {
        if (IsGrounded && !IsClimbing && !GetComponent<PlayerAttack>().IsAttacking && !GetComponent<PlayerAttack>().IsCharging)
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
        if(IsGrounded)
        Character.Move(moveDirection);
    }

    // ************** Input Action Methods **************

    #region Input Actions
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!FreezeMovement)
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
        else
        {
            input_Move = Vector2.zero;
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
                if (IsGrounded && !FreezeMovement)
                {
                    velocity.y = Mathf.Sqrt(JumpHeight * -2 * FallSpeed); //v = Square root of (h * -2 * g)
                    Character.Move(velocity * Time.deltaTime);

                    //if (Animator != null)
                    //{
                    //    Animator.SwitchTo(PlayerAnimation.Jump);
                    //}
                }
                break;

            default:
                break;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
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

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (!FreezeMovement)
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

    // ************** Debug **************
}
