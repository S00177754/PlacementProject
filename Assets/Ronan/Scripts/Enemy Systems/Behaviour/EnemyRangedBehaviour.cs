﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangedBehaviour : EnemyBehaviour
{
    [Header("Projectile")]
    public GameObject ProjectilePrefab;
    public Transform BulletSpawnPoint;
    public float ProjectileVelocity = 5f;

    protected override void Start()
    {
        Navigator = GetComponent<NavMeshAgent>();
        base.Start();

    }

    protected override void Update()
    {
        if (Tracker.IsTracking)
        {
            NeedsRecalculation = true;
            CooldownTimer += Time.deltaTime;

            RotateTo(Tracker.trackedObject);

            if (IsInAttackRange())
            {
                StopMovement();

                if (HasLineOfSight() && IsCooldowned())
                {
                    FireProjectile();
                }
            }
            else
            {
                IsCooldowned();

                MoveTo(Tracker.trackedObject.transform.position);
                Navigator.speed = Stats.Info.ChaseSpeed;
            }
        }
        else if (NextEnemyNode != null)
        {
            if (NextEnemyNode != null)
            {

                if (NeedsRecalculation)
                {
                    RecalculatePath();
                }

                //RotateTo(NextEnemyNode.gameObject);
                MoveTo(NextEnemyNode.gameObject.transform.position);
                Navigator.speed = Stats.Info.PatrolSpeed;

                CooldownTimer = 0f;
            }
        }

        base.Update();
    }

    public void FireProjectile()
    {
        if (ProjectilePrefab != null)
        {
            Rigidbody rb = Instantiate(ProjectilePrefab, BulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = (Tracker.trackedObject.transform.position - BulletSpawnPoint.position).normalized * ProjectileVelocity;
            rb.GetComponent<ProjectileBehaviour>().DamageValue = Stats.Info.Attack;
            Anim.SetTrigger("Attack");
        }
    }

    public void RotateTo(GameObject go)
    {
        Vector3 dis = go.transform.position - transform.position;
        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dis), 10 * Time.deltaTime);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void OnDrawGizmos()
    {
        if (Stats != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Stats.Info.AttackRange);
        }
    }

}
