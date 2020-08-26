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
    public QuestManager Quests;
    

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.Player = GetPlayerData();
        saveData.NearbyEnemies = GetNearbyEnemies();
        saveData.AbilityNodes = GetNodeData();
        saveData.Quests = SaveUtility.GetQuestData(Quests);
        saveData.TravelPointData = GetFastTravelData();
        SaveUtility.SaveToSlot(saveData, GameManager.CurrentSaveSlot);
    }

    public void Load()
    {
        SaveData saveData;

        if(SaveUtility.TryLoadFromSlot(GameManager.CurrentSaveSlot, out saveData))
        {
            //Character Controller has to be disabled?
            PlayerController.Instance.GetComponent<CharacterController>().enabled = false;
            PlayerController.Instance.transform.position = saveData.Player.PlayerPosition;
            PlayerController.Instance.transform.eulerAngles = saveData.Player.PlayerRotation;
            PlayerController.Instance.GetComponent<CharacterController>().enabled = true;

            PlayerController.Instance.GameStats = saveData.Player.PlayerStats;
            PlayerController.Instance.Money = saveData.Player.PlayerMoney;

            PlayerController.Instance.Health = saveData.Player.HP;
            PlayerController.Instance.MP = saveData.Player.MP;
            PlayerController.Instance.CalculateMaxHP();
            PlayerController.Instance.CalculateMaxMP();

            PlayerController.Instance.GetComponent<InventoryManager>().Inventory = saveData.Player.Inventory;
            if(PlayerController.Instance.GetComponent<InventoryManager>().Inventory == null)
            {
                // PlayerController.Instance.GetComponent<InventoryManager>().Inventory = Instantiate(new InventoryObj()); 
                Debug.Log("Need to add default empty");
            }

            //TODO: Get Weapon from item database and assign
            PlayerController.Instance.GetComponent<InventoryManager>().EquipWeapon(saveData.Player.EquippedWeaponID);
            PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotOne = saveData.Player.AccessoryOne;
            PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotTwo = saveData.Player.AccessoryTwo;
            PlayerController.Instance.GetComponent<EquipmentManager>().Loadout.AccessorySlotOne = saveData.Player.AccessoryOne;

            AssignNodeData(saveData.AbilityNodes);

            AssignQuestData(saveData.Quests);

            AssignFastTravelPoints(saveData.TravelPointData);

            foreach (var foe in saveData.NearbyEnemies)
            {
                SpawnEnemy(foe.Transform, foe.RemainingHealth, foe.InfoID);
            }

        }
        else
        {
            Debug.LogError("No Save File Detected");
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

    private List<TravelPointData> GetFastTravelData()
    {
        List<TravelPointData> data = new List<TravelPointData>();

        foreach (var point in TravelPoint.FastTravelPoints)
        {
                data.Add(new TravelPointData() { LocationName = point.Value.LocationName, Unlocked = point.Value.TeleportUnlocked });
        }

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
                    //return;
                }
            }
        }
    }

    private void AssignFastTravelPoints(List<TravelPointData> data)
    {
        foreach (var dataPoint in data)
        {
            foreach (var gamePoint in TravelPoint.FastTravelPoints)
            {
                if(gamePoint.Value.LocationName == dataPoint.LocationName)
                {
                    gamePoint.Value.TeleportUnlocked = dataPoint.Unlocked;
                    break;
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

    private void AssignQuestStepData(QuestStep step, QuestStepData data)
    {
        Debug.Log(string.Concat("Step: ",step.ID,"  Type: ",step.GetType()));

        if(step == null)
        {
            Debug.LogError("Quest Step is null!");
            return;
        }
        else if (data == null)
        {
            Debug.LogError("Quest Step Data is null!");
            return;
        }

        if (step.GetType() == typeof(LocationQuestStep))
        {
            Debug.Log(string.Concat("Quest Step: ",step.ID,"  Data Step: ",data.StepID, "  Complete: ", step.isComplete));
            step.isComplete = data.isComplete;
            step.ID = data.StepID;     
        }
        else if (step.GetType() == typeof(MultiQuantityQuestStep))
        {
            Debug.Log(string.Concat("Quest Step: ", step.ID, "  Data Step: ", data.StepID, "  Complete: ", step.isComplete));
            step.isComplete = data.isComplete;
            step.ID = data.StepID;
            (step as MultiQuantityQuestStep).Counters = (data as MultiQuantityQuestStepData).Counters;
        }
        else if (step.GetType() == typeof(NPCQuestStep))
        {
            Debug.Log(string.Concat("Quest Step: ", step.ID, "  Data Step: ", data.StepID, "  Complete: ",step.isComplete));
            step.isComplete = data.isComplete;
            step.ID = data.StepID;
        }
        else if (step.GetType() == typeof(QuantityQuestStep))
        {
            Debug.Log(string.Concat("Quest Step: ", step.ID, "  Data Step: ", data.StepID, "  Complete: ", step.isComplete));
            step.isComplete = data.isComplete;
            step.ID = data.StepID;
            QuantityQuestStep qStep = (step as QuantityQuestStep);
            QuantityQuestStepData qData = data as QuantityQuestStepData;
            qStep.TargetObtained = qData.TargetObtained;
        }
    }

    private void AssignQuestData(QuestData data)
    {
        foreach (var saveQuest in data.MainQuests)
        {
            foreach (var gameQuest in Quests.MainScenarioQuests)
            {
                if(gameQuest.ID == saveQuest.ID)
                {
                    gameQuest.isActive = saveQuest.IsActive;
                    gameQuest.isFound = saveQuest.IsFound;
                    gameQuest.isComplete = saveQuest.IsComplete;

                    foreach (var saveStep in saveQuest.StepData)
                    {
                        foreach (var gameStep in gameQuest.StepsList)
                        {
                            if(gameStep.ID == saveStep.StepID)
                            {
                                AssignQuestStepData(gameStep, saveStep);
                                break;
                            }
                        }
                    }

                    break;
                }
            }
        }
    }

    #endregion


    
}




[Serializable]
public class SaveData
{
    public PlayerSaveData Player;
    public List<EnemySaveData> NearbyEnemies;
    public List<AbilityNodeData> AbilityNodes;
    public List<TravelPointData> TravelPointData;
    public BossData BossInfo;
    public QuestData Quests;
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

[Serializable]
public class AbilityNodeData
{
    public string IdName;
    public bool Unlocked;
}

[Serializable]
public class BossData
{
    public bool BossOne;
    public bool BossTwo;
    public bool BossThree;
}

[Serializable]
public class QuestData
{
    public List<QuestObjData> MainQuests;
    public List<QuestObjData> SideQuests;
}

[Serializable]
public class QuestObjData
{
    public string ID;
    public bool IsActive;
    public bool IsFound;
    public bool IsComplete;
    public List<QuestStepData> StepData;
    
}

[Serializable]
public class TravelPointData
{
    public string LocationName;
    public bool Unlocked;
}

#region Quest Steps
[Serializable]
public class QuestStepData
{
    public string StepID;
    public bool isComplete;
}

[Serializable]
public class QuantityQuestStepData : QuestStepData
{
    public int TargetObtained;
}

[Serializable]
public class NPCQuestStepData : QuestStepData
{
}

[Serializable]
public class MultiQuantityQuestStepData : QuestStepData
{
    public List<int> Counters;
}

[Serializable]
public class LocationQuestStepData : QuestStepData
{
}

#endregion