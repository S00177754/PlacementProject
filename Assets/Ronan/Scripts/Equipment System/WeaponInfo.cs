﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Sword }

public class WeaponInfo : MonoBehaviour
{
    public WeaponPostion PositioningDetails;

    public WeaponType weaponType;
    public WeaponAttackDetailsObj AttackDetails;

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
