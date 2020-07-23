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
        if(other.gameObject.layer == EntityLayer)
        {
            if (other.GetComponent<Renderer>().IsVisibleFrom(Camera.main))
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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == EntityLayer)
        {
            TargetableObject obj;

            if (other.GetComponent<Renderer>().IsVisibleFrom(Camera.main))
            {
                if (other.TryGetComponent(out obj))
                {
                    if (!Manager.Contains(obj))
                    {
                        Manager.AddTarget(obj);
                    }

                }
            }
            else
            {
                if (other.TryGetComponent(out obj))
                {
                    if (Manager.Contains(obj))
                    {
                        Manager.RemoveTarget(obj);
                    }

                }
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
                if (Manager.Contains(obj))
                {
                    Manager.RemoveTarget(obj);
                }

            }
        }
    }
}
