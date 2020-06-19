using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackerComponent : MonoBehaviour
{
    public SphereCollider TriggerZone;
    public float TrackingRadius = 5f;
    public bool IsTracking = false;
    public GameObject trackedObject;


    public EnemyViewCone ViewCone;

    private void Start()
    {
        TriggerZone.radius = TrackingRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {

            PlayerController player;

            if (other.gameObject.TryGetComponent<PlayerController>(out player) && HasLineOfSight(other))
            {
                IsTracking = true;
                trackedObject = player.gameObject;
                //Debug.Log(string.Concat("Is Now Tracking: ", player));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger && !IsTracking)
        {
            PlayerController player;

            if (other.gameObject.TryGetComponent<PlayerController>(out player) && HasLineOfSight( other))
            {
                IsTracking = true;
                trackedObject = player.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == trackedObject)
        {
            IsTracking = false;
            trackedObject = null;
            //Debug.Log(string.Concat("Stopped Tracking"));
        }
    }

    public Vector3 GetLocationOfTracked()
    {
        if (trackedObject != null)
        {
            return trackedObject.transform.position;
        }
        else
        {
            return Vector3.zero;
        }

      
    }

    public float GetDistanceToTrackedObject()
    {
        return Vector3.Distance(transform.position, trackedObject.transform.position); ;
    }

    public bool HasLineOfSight(Collider other)
    {
        if (ViewCone.IsPlayerInZone)
        {
            Ray lineOfSightRay = new Ray(transform.position, (other.transform.position - transform.position).normalized);

            RaycastHit hitResult;
            bool HasHit = Physics.Raycast(lineOfSightRay, out hitResult, Mathf.Infinity);

            if (HasHit)
            {
                if (hitResult.collider.gameObject == other.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

