using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableObjectFinder : MonoBehaviour
{
    public float Radius = 5f;
    public LayerMask EntityLayer;
    public TargetManager Manager;
 

    private void Start()
    {
        GetComponent<SphereCollider>().radius = Radius;
    }

    private void OnTriggerEnter(Collider other)
    {
            TargetableObject obj;
                if (other.TryGetComponent(out obj))
                {
                    if (!Manager.Contains(obj))
                    {
                        Manager.AddTarget(obj);
                    }
                }
            
        
    }

    private void OnTriggerStay(Collider other)
    {
        TargetableObject obj;
        if (other.TryGetComponent(out obj))
        {
            if (!Manager.Contains(obj))
            {
                Manager.AddTarget(obj);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        TargetableObject obj;
        if (other.TryGetComponent(out obj))
        {
            if (Manager.Contains(obj))
            {
                Manager.RemoveTarget(obj);
            }
        }
    }
}
