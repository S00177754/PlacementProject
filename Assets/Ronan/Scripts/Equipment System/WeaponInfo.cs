using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public WeaponPostion PositioningDetails;

    public bool IsAttacking = false;
    public float DamageValue;

    public void Attack()
    {
        IsAttacking = true;
    }

    public void NotAttacking()
    {
        IsAttacking = false;
    }
}

[Serializable]
public class WeaponPostion
{
    [Header("Attach Points")]
    public AttachPoint SheathedAttachPoint;
    public AttachPoint UnsheathedAttachPoint;

    [Header("Unsheathed Position")]
    public Vector3 UnsheathedPosition;
    public Vector3 UnsheathedRotation;

    [Header("Sheathed Position")]
    public Vector3 SheathedPosition;
    public Vector3 SheathedRotation;
}
