using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SaveLoad : MonoBehaviour
{
    [Header("Data Grab Elements")]
    public PlayerController MainPlayer;
    public EnemyDatabase EnemyDatabase;
    public float EnemySaveRange = 70f;
    public LayerMask EnemyLayer;

    public void Save()
    {

    }

    public void Load()
    {

    }

    private PlayerSaveData GetPlayerData()
    {
        PlayerSaveData data = new PlayerSaveData();
        data.PlayerTransform = MainPlayer.transform;
        data.PlayerStats = MainPlayer.GameStats;
        data.Settings = MainPlayer.Settings;
        data.Inventory = MainPlayer.GetComponent<InventoryManager>().Inventory;
        data.Loadout = MainPlayer.GetComponent<EquipmentManager>().Loadout;
        return data;
    }

    private List<EnemySaveData> GetNearbyEnemies()
    {
        List<EnemySaveData> localEnemies = new List<EnemySaveData>();
        Vector3 playerPos = MainPlayer.transform.position;
        Collider[] hitObjects = Physics.OverlapSphere(playerPos, EnemySaveRange, EnemyLayer);

        EnemyStatsScript enemyController;
        foreach (Collider c in hitObjects)
        {
            if (c.gameObject.TryGetComponent<EnemyStatsScript>(out enemyController))
            {
                localEnemies.Add(
                    new EnemySaveData() 
                    {
                        InfoID = enemyController.Info.ID,
                        RemainingHealth = enemyController.Health,
                        Transform = enemyController.transform
                    }
                );
            }
        }

        return localEnemies;
    }
}

[Serializable]
public class SaveData
{
    public PlayerSaveData Player;
    public List<EnemySaveData> NearbyEnemies; 

}

[Serializable]
public class ListWrapper<T>
{
    public List<T> Items;
}

[Serializable]
public class PlayerSaveData
{
    public Transform PlayerTransform;
    public CharacterStats PlayerStats;
    public PlayerSettings Settings;
    public InventoryObj Inventory;
    public EquipmentLoadout Loadout;
}

[Serializable]
public class EnemySaveData
{
    public Transform Transform;
    public int RemainingHealth;
    public int InfoID;
    
    
}
