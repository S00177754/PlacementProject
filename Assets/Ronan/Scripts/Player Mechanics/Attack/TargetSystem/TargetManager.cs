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
        if(TargetsInRange.Count > 0 && !IsLockedOn)
        {
            SetClosestTargetAsPossible();
        }
        else if (IsLockedOn)
        {
            TargetUIController.SetLockedTarget(FindNearestTarget());
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
        SetClosestTargetAsPossible();
    }

    public void SetClosestTargetAsPossible()
    {
        TargetUIController.SetPossibleTarget(FindNearestTarget());
    }

    public void LockOnTarget()
    {
        IsLockedOn = true;
    }

    public void UnlockOnTarget()
    {
        IsLockedOn = false;
    }

    /// <summary>
    /// Calculates which target in range is the nearest to the player.
    /// </summary>
    /// <returns>Returns the targetable object closest to the player.</returns>
    public TargetableObject FindNearestTarget()
    {
        if (TargetsInRange != null)
        {
            if (TargetsInRange.Count <= 0)
                return null;

            int index = 0;
            float nearest = Vector3.Distance(transform.position, TargetsInRange[0].transform.position);

            for (int i = 0; i < TargetsInRange.Count; i++)
            {
                float distCheck = Vector3.Distance(transform.position, TargetsInRange[i].transform.position);

                if(distCheck > nearest)
                {
                    nearest = distCheck;
                    index = i;
                }
            }

            return TargetsInRange[index];
        }

        return null;
    }
}
