using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public TargettingController TargetUIController;
    public TargetableObjectFinder TargetFinder;
    public List<TargetableObject> TargetsInRange;
    public TargetableObject LockedOnTarget;
    public bool IsLockedOn = false;

    private void Update()
    {
        if (TargetUIController == null)
            return;

        if (!IsLockedOn)
        {
            SetClosestTargetAsPossible();
        }
        else if (IsLockedOn)
        {
            TargetUIController.SetLockedTarget(LockedOnTarget);
        }
    }

    public void AddTarget(TargetableObject newTarget)
    {
        TargetsInRange.Add(newTarget);
        SetClosestTargetAsPossible();
    }

    public bool Contains(TargetableObject newTarget)
    {
        if (TargetsInRange.Contains(newTarget))
        {
            return true;
        }

        return false;
    }

    public void RemoveTarget(TargetableObject newTarget)
    {

        TargetsInRange.Remove(newTarget);
        if(newTarget == LockedOnTarget)
        {
            IsLockedOn = false;
            LockedOnTarget = null;
        }
        SetClosestTargetAsPossible();
    }


    public void SetClosestTargetAsPossible()
    {
        if (TargetUIController == null)
            return;

        if (TargetsInRange.Count > 0)
        {
            TargetUIController.SetPossibleTarget(FindNearestTarget());
        }
        else
        {
            for (int i = 0; i < TargetsInRange.Count; i++)
            {
                if (TargetsInRange[i] == null)
                    TargetsInRange.RemoveAt(i);
            }

            TargetUIController.SetTargetNull();
            UnlockOnTarget();
        }
    }


    public void LockOnTarget()
    {
        if (TargetUIController == null)
            return;

        if (TargetsInRange.Count > 0)
        {
            IsLockedOn = true;
            LockedOnTarget = FindNearestTarget();
            GetComponent<PlayerMovement>().LockOntoObject(LockedOnTarget.transform);
           
        }
    }

    public void UnlockOnTarget()
    {
        IsLockedOn = false;
        LockedOnTarget = null;
        GetComponent<PlayerMovement>().UnlockFromObject();
    }


    /// <summary>
    /// Calculates which target in range is the nearest to the player.
    /// </summary>
    /// <returns>Returns the targetable object closest to the player.</returns>
    public TargetableObject FindNearestTarget()
    {
        if (TargetUIController == null)
            return null;

        for (int i = 0; i < TargetsInRange.Count; i++)
        {
            if (TargetsInRange[i] == null)
                TargetsInRange.RemoveAt(i);
        }

        if (TargetsInRange.Count > 0)
        {
            int index = 0;
            //float nearest = Vector3.Distance(transform.position, TargetsInRange[0].transform.position);
            float nearest = -1;

            for (int i = 0; i < TargetsInRange.Count; i++)
            {   
                if(TargetsInRange[i] != null)
                {
                    float distCheck = Vector3.Distance(transform.position, TargetsInRange[i].transform.position);

                    if(distCheck < nearest || nearest == -1)
                    {
                        nearest = distCheck;
                        index = i;
                    }
                }
            }

            return TargetsInRange[index];
        }
        else
        {
            TargetUIController.SetTargetNull();
        }

        UnlockOnTarget();
        return null;
    }
}
