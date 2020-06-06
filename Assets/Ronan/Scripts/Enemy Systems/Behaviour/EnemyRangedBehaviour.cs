using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangedBehaviour : EnemyBehaviour
{
    private NavMeshAgent Navigator;

    [Header("Projectile")]
    public GameObject ProjectilePrefab;
    public Transform BulletSpawnPoint;
    public float ProjectileVelocity = 5f;

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

            //gameObject.transform.LookAt(Tracker.trackedObject.transform);
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
                transform.position = Vector3.MoveTowards(transform.position, Tracker.trackedObject.transform.position, ChaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (NextEnemyNode != null)
            {

                if (NeedsRecalculation)
                {
                    RecalculatePath();
                }

                RotateTo(NextEnemyNode.gameObject);
                transform.position = Vector3.MoveTowards(transform.position, NextEnemyNode.transform.position, PatrolSpeed * Time.deltaTime);

                CooldownTimer = 0f;
            }
        }
    }

    public void FireProjectile()
    {
        if (ProjectilePrefab != null)
        {
            Rigidbody rb = Instantiate(ProjectilePrefab, BulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = (Tracker.trackedObject.transform.position - BulletSpawnPoint.position).normalized * ProjectileVelocity;
            rb.GetComponent<ProjectileBehaviour>().DamageValue = DamageAmount;
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

    public void RotateTo(GameObject go)
    {
        Vector3 dis = go.transform.position - transform.position;
        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dis), 10 * Time.deltaTime);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

}
