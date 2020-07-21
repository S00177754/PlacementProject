using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventHandler : MonoBehaviour
{
    public void ShowWeapon()
    {
        
        GetComponentInParent<PlayerAttack>().Equipment.HideWeapon();
    }

    public void HideWeapon()
    {
        
        GetComponentInParent<PlayerAttack>().Equipment.ShowWeapon();
    }

    public void ResetAttackTrigger()
    {
        GetComponentInParent<PlayerAnimator>().Animator.ResetTrigger("Attack");
    }

}
