using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBehaviour : MonoBehaviour
{
    [Header("External Components")]
    private NavMeshAgent Navigator;
    private EnemyTrackerComponent Tracker;
    public GameObject AttackZone;

    [Header("Enemy Details")]
    public float AttackRange = 1f;
    public int DamageAmount = 1;
    public float AttackCooldown = 2f;
    private float CooldownTimer = 0f;

    private void Start()
    {
        Navigator = GetComponent<NavMeshAgent>();
        Tracker = GetComponent<EnemyTrackerComponent>();
    }

    private void Update()
    {
        if (Tracker.IsTracking)
        { 
            CooldownTimer += Time.deltaTime;

            if(IsInAttackRange())
            {
                StopMovement();

                if (HasLineOfSight() && IsCooldowned())
                {
                    Attack();
                }
            }
            else
            {
                IsCooldowned();
                MoveTo(Tracker.trackedObject.transform.position);
            }
        }
        else
        {
            CooldownTimer = 0f;
        }
    }

    private void Attack()
    {
        if(AttackZone != null)
        {
            AttackZone.SetActive(true);
            AttackZone.GetComponent<EnemyDamageZone>().SetDamageAmount(DamageAmount);
        }
    }

    private void MoveTo(Vector3 position)
    {
        Navigator.isStopped = false;
        Navigator.SetDestination(position);
    }

    private void StopMovement()
    {
        Navigator.isStopped = true;
        Navigator.SetDestination(transform.position);
    }


    private bool HasLineOfSight()
    {
        Ray lineOfSightRay = new Ray(transform.position, (Tracker.trackedObject.transform.position - transform.position).normalized);

        RaycastHit hitResult;
        bool HasHit = Physics.Raycast(lineOfSightRay, out hitResult ,Mathf.Infinity);

        if (HasHit)
        {
            if (hitResult.collider.gameObject == Tracker.trackedObject)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsCooldowned()
    {
        if (CooldownTimer >= AttackCooldown)
        {
            CooldownTimer = 0f;
            return true;
        }

        return false;
    }

    private bool IsInAttackRange()
    {
        return Tracker.GetDistanceToTrackedObject() <= AttackRange;
    }

}
