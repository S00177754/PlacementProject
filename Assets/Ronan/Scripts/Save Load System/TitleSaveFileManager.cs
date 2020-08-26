using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TitleSaveFileManager : MonoBehaviour
{
    public TitleMenuController TitleMenuController;

    [Header("New Game Data")]
    public CharacterStats StartingStats;
    public WeaponObj StartingWeapon;
    public InventoryObj PlayerInventory;
    public List<ItemObj> StartInventory;
    public List<AbilityTreeNode> NodeList;
    public QuestManager Quests;
    public Vector3 StartPosition;
    public Vector3 StartRotation;



    private PlayerSaveData CreateDefaultPlayerData()
    {
        PlayerSaveData data = new PlayerSaveData();

        data.EquippedWeaponID = StartingWeapon.ID;
        PlayerInventory.Collection.Clear();
        PlayerInventory.AddItem(StartingWeapon, 1);
        StartInventory.ForEach(x => PlayerInventory.AddItem(x, 1));

        data.Inventory = PlayerInventory;

        data.HP = 100;
        data.MP = 100;

        data.PlayerStats = StartingStats;
        data.PlayerPosition = StartPosition;
        data.PlayerRotation = StartRotation;
        return data;
    }

    public List<AbilityNodeData> CreateDefaultNodeData()
    {
        List<AbilityNodeData> nodeData = new List<AbilityNodeData>();
        foreach (var node in NodeList)
        {
            nodeData.Add(new AbilityNodeData() { IdName = node.IdName, Unlocked = false });
        }
        return nodeData;
    }

    public QuestData CreateDefaultQuestData()
    {
        QuestData data = new QuestData();
        data.MainQuests = new List<QuestObjData>();
        foreach (var MQ in Quests.MainScenarioQuests)
        {
            data.MainQuests.Add(SaveUtility.GetQuestObject(MQ));
        }

        data.SideQuests = new List<QuestObjData>();
        foreach (var SQ in Quests.SideQuests)
        {
            data.SideQuests.Add(SaveUtility.GetQuestObject(SQ));
        }

        OverwriteQuests(ref data.MainQuests);
        OverwriteQuests(ref data.SideQuests);

        return data;
    }

    public void OverwriteQuests(ref List<QuestObjData> quests)
    {
        foreach (var mq in quests)
        {
            mq.IsActive = false;
            mq.IsFound = false;
            mq.IsComplete = false;

            foreach (var step in mq.StepData)
            {
                step.isComplete = false;

                if (step.GetType() == typeof(MultiQuantityQuestStepData))
                {
                    (step as MultiQuantityQuestStepData).Counters.ForEach(c => c = 0);
                }
                else if (step.GetType() == typeof(QuantityQuestStepData))
                {
                    (step as QuantityQuestStepData).TargetObtained = 0;
                }
            }
        }
    }

    private SaveData NewGameData()
    {
        SaveData data = new SaveData();
        data.Player = CreateDefaultPlayerData();
        data.NearbyEnemies = new List<EnemySaveData>();
        data.AbilityNodes = CreateDefaultNodeData();
        data.Quests = CreateDefaultQuestData();
        return data;
    }

    public void CreateNewGame(int saveSlot)
    {
        SaveUtility.SaveToSlot(NewGameData(), saveSlot);
    }

}

public static class SaveUtility
{
    public static void SaveToSlot(SaveData data,int slot)
    {
        if(data == null)
        {
            Debug.LogError("Save data is null.");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/SaveDataFile_{slot}.crescent";
        Debug.Log(path);
        FileStream fs = new FileStream(path, FileMode.Create);

        string json = JsonUtility.ToJson(data);

        bf.Serialize(fs, json);
        fs.Close();
    }

    public static SaveData LoadFromSlot(int slot)
    {
        string path = Application.persistentDataPath + $"/SaveDataFile_{slot}.crescent";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            string json = bf.Deserialize(fs) as string;
            SaveData data = JsonUtility.FromJson(json, typeof(SaveData)) as SaveData;
            return data;
        }
        else
        {
            Debug.LogError("Save file does not exist.");
            return null;
        }
    }

    public static bool TryLoadFromSlot(int slot, out SaveData saveData)
    {
        string path = Application.persistentDataPath + $"/SaveDataFile_{slot}.crescent";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            string json = bf.Deserialize(fs) as string;
            saveData = JsonUtility.FromJson(json, typeof(SaveData)) as SaveData;
            return true;
        }

        Debug.Log("Save file does not exist.");
        saveData = null;
        return false;    
    }

    public static bool CheckForFile(int slot)
    {
        string path = Application.persistentDataPath + $"/SaveDataFile_{slot}.crescent";
        if (File.Exists(path))
        {
            return true;
        }

        return false;

    }


    public static QuestStepData GetQuestStepData(QuestStep step)
    {
        if (step.GetType() == typeof(LocationQuestStep))
        {
            return new LocationQuestStepData()
            {
                isComplete = step.isComplete,
                StepID = step.ID
            };
        }
        else if (step.GetType() == typeof(MultiQuantityQuestStep))
        {
            return new MultiQuantityQuestStepData()
            {
                isComplete = step.isComplete,
                StepID = step.ID,
                Counters = (step as MultiQuantityQuestStep).Counters,
            };
        }
        else if (step.GetType() == typeof(NPCQuestStep))
        {
            return new NPCQuestStepData()
            {
                isComplete = step.isComplete,
                StepID = step.ID,
            };
        }
        else if (step.GetType() == typeof(QuantityQuestStep))
        {
            return new QuantityQuestStepData()
            {
                isComplete = step.isComplete,
                StepID = step.ID,
                TargetObtained = (step as QuantityQuestStep).TargetObtained,
            };
        }

        return null;
    }

    public static QuestObjData GetQuestObject(Quest quest)
    {
        QuestObjData data = new QuestObjData();
        data.ID = quest.ID;
        data.IsActive = quest.isActive;
        data.IsComplete = quest.isComplete;
        data.IsFound = quest.isFound;
        data.StepData = new List<QuestStepData>();

        foreach (var step in quest.StepsList)
        {
            data.StepData.Add(GetQuestStepData(step));
        }

        return data;
    }

    public static QuestData GetQuestData(QuestManager manager)
    {
        QuestData data = new QuestData();
        data.MainQuests = new List<QuestObjData>();
        foreach (var MQ in manager.MainScenarioQuests)
        {
            data.MainQuests.Add(GetQuestObject(MQ));
        }

        data.SideQuests = new List<QuestObjData>();
        foreach (var SQ in manager.SideQuests)
        {
            data.SideQuests.Add(GetQuestObject(SQ));
        }

        return data;
    }
}
