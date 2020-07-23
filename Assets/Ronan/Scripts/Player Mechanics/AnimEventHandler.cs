using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventHandler : MonoBehaviour
{
    public void ShowWeapon()
    {
        
        PlayerController.Instance.GetComponent<PlayerAttack>().Equipment.HideWeapon();
    }

    public void HideWeapon()
    {

        PlayerController.Instance.GetComponent<PlayerAttack>().Equipment.ShowWeapon();
    }

    public void ResetAttackTrigger()
    {
        PlayerController.Instance.GetComponent<PlayerAnimator>().Animator.ResetTrigger("Attack");
    }

}
