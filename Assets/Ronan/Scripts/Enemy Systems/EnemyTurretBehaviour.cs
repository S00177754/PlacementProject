using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTrackerComponent))]
public class EnemyTurretBehaviour : MonoBehaviour
{
    public Transform TurretRotateObject;
    public Transform BulletSpawnPoint;
    public GameObject Projectile;
    public float ProjectileVelocity = 5f;
    public float AttackCooldown = 2f;

    private EnemyTrackerComponent Track;
    private float CooldownTimer = 0f;

    private void Start()
    {
        Track = GetComponent<EnemyTrackerComponent>();
    }

    private void Update()
    {
        if (Track.IsTracking)
        {
            TurretRotateObject.transform.LookAt(Track.trackedObject.transform);
            CooldownTimer += Time.deltaTime;

            if (HasLineOfSight() && IsCooldowned())
            {
                Fire();
            }

        }
        else
        {
            CooldownTimer = 0f;
        }
    }

    public void Fire()
    {
        Debug.Log(string.Concat("Fired at ",Track.trackedObject));

        Rigidbody rb = Instantiate(Projectile, BulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.velocity = (Track.trackedObject.transform.position - BulletSpawnPoint.position).normalized * ProjectileVelocity;
        //Invoke("Fire", FireTime);
    }

    private bool HasLineOfSight()
    {
        Ray lineOfSightRay = new Ray(BulletSpawnPoint.position, (Track.trackedObject.transform.position - BulletSpawnPoint.position).normalized);

        
       RaycastHit[] hitResult = Physics.RaycastAll(lineOfSightRay, Mathf.Infinity);

        if (hitResult.Length > 0)
        {
            for (int i = 0; i < hitResult.Length; i++)
            {
                if (hitResult[i].collider.gameObject == Track.trackedObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsCooldowned()
    {
        if(CooldownTimer >= AttackCooldown)
        {
            CooldownTimer = 0f;
            return true;
        }

        return false;
    }
}
