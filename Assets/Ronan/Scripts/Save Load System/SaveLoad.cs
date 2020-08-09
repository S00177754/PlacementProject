using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SaveLoad : MonoBehaviour
{
    [Header("Data Grab Elements")]
    //public PlayerController MainPlayer; //Could replace with PlayerController.Instance thanks to the singleton pattern
    public EnemyDatabase EnemyDatabase;
    public float EnemySaveRange = 70f;
    public LayerMask EnemyLayer;

    

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.Player = GetPlayerData();
        saveData.NearbyEnemies = GetNearbyEnemies();

    }

    public void Load()
    {

    }

    #region Save Grab Logic
    private PlayerSaveData GetPlayerData()
    {
        PlayerSaveData data = new PlayerSaveData();

        data.PlayerPosition = PlayerController.Instance.transform.position;
        data.PlayerRotation = new Vector3(PlayerController.Instance.transform.rotation.x, PlayerController.Instance.transform.rotation.y, PlayerController.Instance.transform.rotation.z);
        
        data.PlayerStats = PlayerController.Instance.GameStats;
        data.PlayerMoney = PlayerController.Instance.Money;

        data.HP = PlayerController.Instance.Health;
        data.MP = PlayerController.Instance.MP;

        data.Inventory = PlayerController.Instance.GetComponent<InventoryManager>().Inventory;
        data.EquippedWeaponID = PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.EquippedWeapon.ItemID;
        data.AccessoryOne = PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotOne;
        data.AccessoryTwo = PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotTwo;
        data.AccessoryThree = PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotThree;
        //TODO Redo the way items are saved and loaded back
        return data;
    }

    private List<EnemySaveData> GetNearbyEnemies()
    {
        List<EnemySaveData> localEnemies = new List<EnemySaveData>();
        Vector3 playerPos = PlayerController.Instance.transform.position;
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

    #endregion


    
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
    public Vector3 PlayerPosition;
    public Vector3 PlayerRotation;

    public CharacterStats PlayerStats;
    public InventoryObj Inventory;
    public int PlayerMoney;

    public int HP;
    public int MP;

    public int EquippedWeaponID;
    public BaubleObj AccessoryOne;
    public BaubleObj AccessoryTwo;
    public BaubleObj AccessoryThree;
}

[Serializable]
public class EnemySaveData
{
    public Transform Transform;
    public int RemainingHealth;
    public int InfoID; ///This ID matches up with enemy database, used to spawn correct enemy type back in
    
    
}
