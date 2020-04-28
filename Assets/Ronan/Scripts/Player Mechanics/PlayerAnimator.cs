using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimation { Idle, Walk, Jump }

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

    }


    public void SwitchTo(PlayerAnimation animation)
    {
        switch (animation)
        {
            default:
            case PlayerAnimation.Idle:
                Animator.ResetTrigger("Walking");
                //Animator.ResetTrigger("Jump");
                Animator.SetTrigger("Idle");
                break;

            case PlayerAnimation.Walk:
                Animator.ResetTrigger("Idle");
               // Animator.ResetTrigger("Jump");
                Animator.SetTrigger("Walking");
                break;

            case PlayerAnimation.Jump:
                Animator.ResetTrigger("Idle");
                Animator.ResetTrigger("Walking");
                Animator.SetTrigger("Jump");
                break;
        }
    }
}
