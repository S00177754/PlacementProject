using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDroneBehaviour : EnemyBehaviour
{
    public float HoverDistance = 1f;
    public float DescentSpeed = 1f;

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
            
            if (IsInAttackRange())
            {

            }
            else
            {
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

            //transform.position = Vector3.MoveTowards(transform.position, NextEnemyNode.transform.position, PatrolSpeed * Time.deltaTime);
            MoveTo(NextEnemyNode.transform.position);
            Navigator.speed = PatrolSpeed;
        }
    }

    public float CheckDistanceToGround()
    {
        RaycastHit hitResult;
        bool HasHit = Physics.Raycast(gameObject.transform.position,Vector3.down, out hitResult, Mathf.Infinity);
        return hitResult.distance;
    }
}
