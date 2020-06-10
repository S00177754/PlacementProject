using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewCone : MonoBehaviour
{
    public bool IsPlayerInZone = false;
    public bool EntityInSight = false;

    public PlayerController player;
    public int EntityLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == EntityLayer && !other.isTrigger)
        {
            EntityInSight = true;

            if(other.TryGetComponent(out player))
            {
                IsPlayerInZone = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == EntityLayer && !other.isTrigger)
        {
            EntityInSight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == EntityLayer && !other.isTrigger)
        {
            EntityInSight = false;

            if (other.TryGetComponent(out player))
            {
                IsPlayerInZone = false;
                player = null;
            }
        }
    }
}
