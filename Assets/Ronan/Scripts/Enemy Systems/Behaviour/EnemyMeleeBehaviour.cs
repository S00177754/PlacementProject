﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeBehaviour : EnemyBehaviour
{
    [Header("External Components")]
    
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

                MoveTo(Tracker.trackedObject.transform.position);
                Navigator.speed = ChaseSpeed;
            }
        }
        else
        {
            if (NeedsRecalculation)
            {
                RecalculatePath();
            }

            MoveTo(NextEnemyNode.transform.position);
            Navigator.speed = PatrolSpeed;

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

   

 


}