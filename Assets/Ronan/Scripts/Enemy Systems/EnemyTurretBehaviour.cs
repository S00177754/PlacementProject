using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretType { Projectile, Beam}

[RequireComponent(typeof(EnemyTrackerComponent))]
public class EnemyTurretBehaviour : MonoBehaviour
{
    public TurretType Type = TurretType.Projectile;
    public Transform TurretRotateObject;
    public Transform BulletSpawnPoint;
    private EnemyTrackerComponent Track;

    [Header("Beam")]
    public LineRenderer LineRender;
    public float BeamActiveTime = 2f;
    public float BeamTimer = 0f;

    [Header("Projectile")]
    public GameObject ProjectilePrefab;
    public float ProjectileVelocity = 5f;

    [Header("Turrent Variables")]
    public int DamageAmount = 1;
    public float AttackCooldown = 2f;
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
                if(Type == TurretType.Projectile)
                {
                    FireProjectile();
                }
            }

        }
        else
        {
            CooldownTimer = 0f;
        }
    }

    public void FireProjectile()
    {
        if (ProjectilePrefab != null)
        {
            Rigidbody rb = Instantiate(ProjectilePrefab, BulletSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.velocity = (Track.trackedObject.transform.position - BulletSpawnPoint.position).normalized * ProjectileVelocity;
            rb.GetComponent<ProjectileBehaviour>().DamageValue = DamageAmount;
        }
    }

    public void ActivateBeam()
    {
        if(LineRender != null)
        {
            BeamTimer = 0f;
            LineRender.gameObject.SetActive(true);
        }
    }

    public void DisableBeam()
    {
        if(BeamTimer >= BeamActiveTime)
        {
            LineRender.gameObject.SetActive(true);
            BeamTimer = 0f;
        }
    }

    private bool HasLineOfSight()
    {
        // Ray lineOfSightRay = new Ray(BulletSpawnPoint.position, (Track.trackedObject.transform.position - BulletSpawnPoint.position).normalized);


        //RaycastHit[] hitResult = Physics.RaycastAll(lineOfSightRay, Mathf.Infinity);

        // if (hitResult.Length > 0)
        // {
        //     for (int i = 0; i < hitResult.Length; i++)
        //     {
        //         if (hitResult[i].collider.gameObject == Track.trackedObject)
        //         {
        //             return true;
        //         }
        //     }
        // }

        Ray lineOfSightRay = new Ray(transform.position, (Track.trackedObject.transform.position - transform.position).normalized);

        RaycastHit hitResult;
        bool HasHit = Physics.Raycast(lineOfSightRay, out hitResult, Mathf.Infinity);

        if (HasHit)
        {
            if (hitResult.collider.gameObject == Track.trackedObject)
            {
                return true;
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
