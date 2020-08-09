using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SaveLoad : MonoBehaviour
{
    [Header("Data Grab Elements")]
    //public PlayerController MainPlayer; //Could replace with PlayerController.Instance thanks to the singleton pattern
    public EnemyDatabase enemyDatabase;
    public float EnemySaveRange = 70f;
    public LayerMask EnemyLayer;
    public List<AbilityTreeNode> NodeList;

    

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.Player = GetPlayerData();
        saveData.NearbyEnemies = GetNearbyEnemies();
        saveData.AbilityNodes = GetNodeData();

    }

    public void Load()
    {
        SaveData saveData;

        if(SaveUtility.TryLoadFromSlot(GameManager.CurrentSaveSlot, out saveData))
        {
            PlayerController.Instance.transform.position = saveData.Player.PlayerPosition;
            PlayerController.Instance.transform.eulerAngles = saveData.Player.PlayerRotation;

            PlayerController.Instance.GameStats = saveData.Player.PlayerStats;
            PlayerController.Instance.Money = saveData.Player.PlayerMoney;

            PlayerController.Instance.Health = saveData.Player.HP;
            PlayerController.Instance.MP = saveData.Player.MP;

            PlayerController.Instance.GetComponent<InventoryManager>().Inventory = saveData.Player.Inventory;
            //TODO: Get Weapon from item database and assign
            //PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.EquippedWeapon
            PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotOne = saveData.Player.AccessoryOne;
            PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotTwo = saveData.Player.AccessoryTwo;
            PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotOne = saveData.Player.AccessoryOne;

            AssignNodeData(saveData.AbilityNodes);

            foreach (var foe in saveData.NearbyEnemies)
            {
                SpawnEnemy(foe.Transform, foe.RemainingHealth, foe.InfoID);
            }

        }
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

    private List<AbilityNodeData> GetNodeData()
    {
        List<AbilityNodeData> list = new List<AbilityNodeData>();

        for (int i = 0; i < NodeList.Count; i++)
        {
            list.Add(new AbilityNodeData() { IdName = NodeList[i].IdName, Unlocked = NodeList[i].NodeUnlocked });
        }

        return list;
    }

    //LOAD
    private void AssignNodeData(List<AbilityNodeData> data)
    {
        foreach (var saveNode in data)
        {
            foreach (var gameNode in NodeList)
            {
                if(saveNode.IdName == gameNode.IdName)
                {
                    gameNode.NodeUnlocked = saveNode.Unlocked;
                    return;
                }
            }
        }
    }

    private void SpawnEnemy(Transform transform,int health, int enemyID)
    {
        EnemyInfo info = enemyDatabase.GetInfo[enemyID];

        GameObject go = Instantiate(info.Prefab);
        go.transform.position = transform.position;
        go.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        EnemyStatsScript enemy = go.GetComponent<EnemyStatsScript>();
        enemy.Health = health;
    }

    #endregion


    
}




[Serializable]
public class SaveData
{
    public PlayerSaveData Player;
    public List<EnemySaveData> NearbyEnemies;
    public List<AbilityNodeData> AbilityNodes;
    public BossData BossInfo;
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

public class AbilityNodeData
{
    public string IdName;
    public bool Unlocked;
}

public class BossData
{
    public bool BossOne;
    public bool BossTwo;
    public bool BossThree;
}

public class QuestData
{
    public int QuestID;
    public bool IsActive;
    public bool IsComplete;
    public QuestStep ActiveStep; //Maybe int id
    public QuestStep NextStep; //Maybe int id
}
