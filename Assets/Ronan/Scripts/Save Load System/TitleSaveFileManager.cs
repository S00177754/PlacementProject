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
    public List<ItemObj> StartInventory;
    public Vector3 StartPosition;
    public Vector3 StartRotation;



    private PlayerSaveData CreateDefaultPlayerData()
    {
        PlayerSaveData data = new PlayerSaveData();

        data.Loadout.EquippedWeapon = StartingWeapon.WeaponPrefab.GetComponent<WeaponInfo>();
        data.Inventory.AddItem(StartingWeapon, 1);
        StartInventory.ForEach(x => data.Inventory.AddItem(x, 1));

        data.PlayerStats = StartingStats;

        data.PlayerTransform.position = StartPosition;
        data.PlayerTransform.eulerAngles = StartRotation;
        return data;
    }

    private SaveData NewGameData()
    {
        SaveData data = new SaveData();
        data.Player = CreateDefaultPlayerData();
        data.NearbyEnemies = new List<EnemySaveData>();
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
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/SaveDataFile_{slot}.crescent";
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
}
