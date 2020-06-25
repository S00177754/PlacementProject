using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Info", menuName = "Combat System/Enemy Info")]
[Serializable]
public class EnemyInfo : ScriptableObject
{
    public int ID;
    public GameObject Prefab;

    [Header("Enemy Attributes")]
    public int Health;
    public int Attack;
    public float AttackRange;
    public float AttackCooldown;

    [Header("Speed")]
    public float PatrolSpeed;
    public float ChaseSpeed;

}
