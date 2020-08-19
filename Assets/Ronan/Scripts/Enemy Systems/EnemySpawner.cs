using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static public int EntityCap = 10;

    public GameObject SpawnEntity;
    public float SpawnerCooldown = 10f;
    public EnemyPathNode EnemyPath;
    public List<Transform> SpawnPoints;
    public List<EnemyPathNode> PossibleEnemyPaths;

    private bool IsPlayerInRange = false;
    private float SpawnClock = 0f;

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
        
        if (!IsPlayerInRange || EnemyBehaviour.EnemyCount >= EntityCap || SpawnEntity == null)
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
        GameObject go = Instantiate(SpawnEntity,RandomSpawnPoint().position,Quaternion.identity);
        go.GetComponent<EnemyBehaviour>().NextEnemyNode = EnemyPath;
        go.GetComponent<EnemyBehaviour>().GetFullPath();
    }

    private Transform RandomSpawnPoint()
    {
        return SpawnPoints[Random.Range(0, SpawnPoints.Count)];
    }
}
