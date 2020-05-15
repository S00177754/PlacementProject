using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventHandler : MonoBehaviour
{
    public void SheatheWeapon()
    {
        
        GetComponentInParent<PlayerAttack>().SheathWeapon();
    }

    public void UnSheatheWeapon()
    {
        
        GetComponentInParent<PlayerAttack>().UnsheathWeapon();
    }

    public void ResetAttackTrigger()
    {
        GetComponentInParent<PlayerAnimator>().Animator.ResetTrigger("Attack");
    }
}
