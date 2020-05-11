using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Info Object", menuName = "Combat System/Attack Info")]
[Serializable]
public class AttackInfoObj : ScriptableObject
{
    [Header("Core Details")]
    public string AttackZoneName;
    public int DamageAmount;

    [Header("Sphere Trigger Settings")]
    public float AttackRadius = -1f;
}
