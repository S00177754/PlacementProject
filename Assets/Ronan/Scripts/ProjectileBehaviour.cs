using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public int DamageValue = 1;

    public void OnCollisionEnter(Collision collision)
    {
        PlayerController player;
        if(collision.gameObject.TryGetComponent(out player))
        {
            player.ApplyDamage(DamageValue);
        }

        Destroy(gameObject);
    }
}
