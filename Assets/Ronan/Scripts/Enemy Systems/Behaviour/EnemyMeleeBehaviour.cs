using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBehaviour : EnemyBehaviour
{
    [Header("External Components")]
    private NavMeshAgent Navigator;
    public GameObject AttackZone;

    protected override void Start()
    {
        Navigator = GetComponent<NavMeshAgent>();
        base.Start();
        
    }

    private void Update()
    {
        if (Tracker.IsTracking)
        {
            NeedsRecalculation = true;
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
                //MoveTo(Tracker.trackedObject.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, Tracker.trackedObject.transform.position, ChaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (NeedsRecalculation)
            {
                RecalculatePath();
            }

            transform.position = Vector3.MoveTowards(transform.position, NextEnemyNode.transform.position, PatrolSpeed * Time.deltaTime);

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

    }


}
