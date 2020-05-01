using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimation { Idle, Walk, Run, Jump, Fall, Land, Crouch }

public class PlayerAnimator : MonoBehaviour
{
    public Animator Animator;
    public PlayerAnimation CurrentAnimation;
    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Animator.SetBool("IsJumping", movement.IsJumping);
        Animator.SetBool("IsFalling", movement.IsFalling);
        Animator.SetBool("IsCrouching", movement.IsCrouching);
    }


    public void SwitchTo(PlayerAnimation animation)
    {
        switch (animation)
        {
            default:
                break;

            case PlayerAnimation.Idle:
                Animator.SetFloat("Speed", 0);
                break;

            case PlayerAnimation.Walk:
                Animator.SetFloat("Speed", 2);
                break;

            case PlayerAnimation.Run:
                Animator.SetFloat("Speed", 6);
                break;

            //case PlayerAnimation.Jump:
            //    Animator.SetBool("Jump",movement.IsJumping);
            //    break;

            //case PlayerAnimation.Fall:
            //    Animator.SetFloat("Speed", 6);
            //    break;

            case PlayerAnimation.Land:
                Animator.SetTrigger("FallToLand");
                break;

        }
    }
}
