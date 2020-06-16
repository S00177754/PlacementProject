using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyTrackerComponent))]
public class EnemyBehaviour : MonoBehaviour
{
    protected NavMeshAgent Navigator;
    protected EnemyTrackerComponent Tracker;
    public PathUserID PathID;

    [Header("Enemy Details")]
    public float AttackRange = 1f;
    public int DamageAmount = 1;
    public float AttackCooldown = 2f;
    protected float CooldownTimer = 0f;

    public float PatrolSpeed = 2f;
    public float ChaseSpeed = 4f;

    public List<EnemyPathNode> EnemyPath;
    public EnemyPathNode NextEnemyNode;
    protected bool NeedsRecalculation = false;


    protected virtual void Start()
    {
        Tracker = GetComponent<EnemyTrackerComponent>();

        if(NextEnemyNode != null)
        GetFullPath();
    }

    public bool HasLineOfSight()
    {
        Ray lineOfSightRay = new Ray(transform.position, (Tracker.trackedObject.transform.position - transform.position).normalized);

        RaycastHit hitResult;
        bool HasHit = Physics.Raycast(lineOfSightRay, out hitResult, Mathf.Infinity);

        if (HasHit)
        {
            if (hitResult.collider.gameObject == Tracker.trackedObject)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsCooldowned()
    {
        if (CooldownTimer >= AttackCooldown)
        {
            CooldownTimer = 0f;
            return true;
        }

        return false;
    }

    public bool IsInAttackRange()
    {
        return Tracker.GetDistanceToTrackedObject() <= AttackRange;
    }



    //******* Pathfinding *********

    public void GetFullPath()
    {
        EnemyPath.Add(NextEnemyNode);
        AddNextNode(NextEnemyNode);   
    }

    public void AddNextNode(EnemyPathNode node)
    {
        if (!EnemyPath.Contains(node.NextNode))
        {
            EnemyPath.Add(node.NextNode);
            AddNextNode(node.NextNode);
        }

    }

    public void RecalculatePath()
    {
        EnemyPathNode nearestNode = EnemyPath[0];
        float distanceToNode = Vector3.Distance(transform.position, nearestNode.transform.position);

        foreach (EnemyPathNode node in EnemyPath)
        {
            if(Vector3.Distance(transform.position, node.transform.position) <= distanceToNode)
            {
                nearestNode = node;
                distanceToNode = Vector3.Distance(transform.position, node.transform.position);
            }
        }

        NextEnemyNode = nearestNode;
        NeedsRecalculation = false;
    }

    protected void MoveTo(Vector3 position)
    {
        Navigator.isStopped = false;
        Navigator.SetDestination(position);
    }

    protected void StopMovement()
    {
        Navigator.isStopped = true;

    }
}
