using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTrackerComponent))]
public class EnemyTurretBehaviour : MonoBehaviour
{
    public Transform TurretRotateObject;
    public Transform BulletSpawnPoint;

    private EnemyTrackerComponent Track;

    private void Update()
    {
        
    }
}
