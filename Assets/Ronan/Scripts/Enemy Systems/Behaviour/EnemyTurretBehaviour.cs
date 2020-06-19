using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretType { Projectile, Beam}


public class EnemyTurretBehaviour : EnemyBehaviour
{
    public TurretType Type = TurretType.Projectile;
    public Transform TurretRotateObject;
    public Transform BulletSpawnPoint;

    [Header("Beam")]
    public LineRenderer LineRender;
    public float BeamActiveTime = 2f;
    public float BeamTimer = 0f;

    [Header("Projectile")]
    public GameObject ProjectilePrefab;
    public float ProjectileVelocity = 5f;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (Tracker.IsTracking)
        {
            TurretRotateObject.transform.LookAt(Tracker.trackedObject.transform);
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
            rb.velocity = (Tracker.trackedObject.transform.position - BulletSpawnPoint.position).normalized * ProjectileVelocity;
            rb.GetComponent<ProjectileBehaviour>().DamageValue = Stats.Info.Attack;
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

}
