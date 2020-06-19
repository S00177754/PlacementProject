using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static public int EntityCap = 10;

    public GameObject SpawnEntity;
    public float SpawnerCooldown = 10f;
    public float SpawnRange = 30f;
    public EnemyPathNode EnemyPath;

    private bool IsPlayerInRange = false;
    private float SpawnClock = 0f;

    private void Start()
    {
        SpawnRange = 30f;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player;
        if(other.gameObject.TryGetComponent<PlayerController>(out player))
        {
            SpawnClock = 0f;
            IsPlayerInRange = true;
        }
        return;
    }

    private void Update()
    {
        if (!IsPlayerInRange || EnemyBehaviour.EnemyCount >= EntityCap)
            return;
        else
        {
            SpawnClock += Time.deltaTime;
            if (SpawnClock >= SpawnerCooldown)
            {
                SpawnEnemy();
                SpawnClock = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player;
        if (other.gameObject.TryGetComponent<PlayerController>(out player))
        {
            SpawnClock = 0f;
            IsPlayerInRange = false;
        }
        return;
    }

    public void SpawnEnemy()
    {
        //Debug.Log("Spawning");
        GameObject go = Instantiate(SpawnEntity,transform.position,Quaternion.identity);
        go.GetComponent<EnemyBehaviour>().NextEnemyNode = EnemyPath;
        go.GetComponent<EnemyBehaviour>().GetFullPath();
    }
}
