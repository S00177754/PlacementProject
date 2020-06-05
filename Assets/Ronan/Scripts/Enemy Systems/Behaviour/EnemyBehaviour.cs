using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTrackerComponent))]
public class EnemyBehaviour : MonoBehaviour
{
    protected EnemyTrackerComponent Tracker;

    [Header("Enemy Details")]
    public float AttackRange = 1f;
    public int DamageAmount = 1;
    public float AttackCooldown = 2f;
    protected float CooldownTimer = 0f;

    public List<EnemyPathNode> EnemyPath;
    public EnemyPathNode NextEnemyNode;

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
}
