using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageZone : MonoBehaviour
{
    private int DamageValue;

    public void SetDamageAmount(int amount)
    {
        DamageValue = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player;
        if (other.gameObject.TryGetComponent(out player))
        {
            player.ApplyDamage(DamageValue);
            gameObject.SetActive(false);
        }
    }
}
