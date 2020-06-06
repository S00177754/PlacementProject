using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneBehaviour : EnemyBehaviour
{
    public float HoverDistance = 1f;
    public float DescentSpeed = 1f;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if(CheckDistanceToGround() >= HoverDistance + (HoverDistance / 10))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (DescentSpeed * Time.deltaTime), transform.position.z);
        }
        else if (CheckDistanceToGround() < HoverDistance - (HoverDistance / 10))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (DescentSpeed * Time.deltaTime), transform.position.z);
        }

        if (Tracker.IsTracking)
        {
            NeedsRecalculation = true;
            
            if (IsInAttackRange())
            {

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Tracker.trackedObject.transform.position, ChaseSpeed * Time.deltaTime) ;
            }

        }
        else
        {
            if (NeedsRecalculation)
            {
                RecalculatePath();
            }

            transform.position = Vector3.MoveTowards(transform.position, NextEnemyNode.transform.position, PatrolSpeed * Time.deltaTime);
        }
    }

    public float CheckDistanceToGround()
    {
        RaycastHit hitResult;
        bool HasHit = Physics.Raycast(gameObject.transform.position,Vector3.down, out hitResult, Mathf.Infinity);
        return hitResult.distance;
    }
}
