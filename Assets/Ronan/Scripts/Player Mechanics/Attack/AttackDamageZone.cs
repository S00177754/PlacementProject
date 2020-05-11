using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageZone : MonoBehaviour
{
    public string ZoneName;
    private bool IsDealingDamage = false;
    private int damageAmount = 1;

    public void DealDamage(int damageValue)
    {
        gameObject.SetActive(true);
        damageAmount = damageValue;
        IsDealingDamage = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsDealingDamage)
        {
            IsDealingDamage = false;
            gameObject.SetActive(false);
        }
    }


}
