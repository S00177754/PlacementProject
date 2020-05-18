using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackerComponent : MonoBehaviour
{
    public SphereCollider TriggerZone;
    public float TrackingRadius = 5f;
    public bool IsTracking = false;
    private GameObject trackedObject;

    private void Start()
    {
        TriggerZone.radius = TrackingRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player;

        if(other.gameObject.TryGetComponent<PlayerController>(out player))
        {
            IsTracking = true;
            trackedObject = player.gameObject;
            Debug.Log(string.Concat("Is Now Tracking: ",player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == trackedObject)
        {
            IsTracking = false;
            trackedObject = null;
            Debug.Log(string.Concat("Stopped Tracking"));
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

}
