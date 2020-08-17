using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyTrackerComponent))]
public class EnemyBehaviour : MonoBehaviour
{
    public static int EnemyCount = 0;

    protected NavMeshAgent Navigator;
    protected EnemyTrackerComponent Tracker;
    protected EnemyStatsScript Stats;

    [Header("Enemy Details")]
    protected float CooldownTimer = 0f;

    public string PathID;
    public List<EnemyPathNode> EnemyPath;
    public EnemyPathNode NextEnemyNode;
    protected bool NeedsRecalculation = false;


    protected virtual void Start()
    {

        EnemyCount++;
        Stats = GetComponent<EnemyStatsScript>();
        Tracker = GetComponent<EnemyTrackerComponent>();

        if(NextEnemyNode != null)
        {
            GetFullPath();
        }
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(PlayerController.Instance.transform.position,transform.position) > PlayerSettings.EnemyDespawnRange)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        EnemyCount--;
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
        if (CooldownTimer >= Stats.Info.AttackCooldown)
        {
            CooldownTimer = 0f;
            return true;
        }

        return false;
    }

    public bool IsInAttackRange()
    {
        return Tracker.GetDistanceToTrackedObject() <= Stats.Info.AttackRange;
    }



    //******* Pathfinding *********

    public void GetFullPath()
    {
        EnemyPath.Clear();
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
