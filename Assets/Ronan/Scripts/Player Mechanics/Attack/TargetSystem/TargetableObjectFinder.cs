using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableObjectFinder : MonoBehaviour
{
    public float Radius = 5f;
    public LayerMask EntityLayer;
    public TargetManager Manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == EntityLayer)
        {
            TargetableObject obj;
            if(other.TryGetComponent(out obj))
            {
                Manager.AddTarget(obj);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == EntityLayer)
        {
            TargetableObject obj;
            if (other.TryGetComponent(out obj))
            {
                Manager.RemoveTarget(obj);
            }
        }
    }
}
