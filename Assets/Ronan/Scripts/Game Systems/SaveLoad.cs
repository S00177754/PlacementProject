using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [Header("Data Grab Elements")]
    public PlayerController MainPlayer;

    public void Save()
    {

    }

    public void Load()
    {

    }

    private PlayerData GetPlayerData(PlayerController MainPlayer)
    {
        PlayerData data = new PlayerData();
        data.PlayerTransform = MainPlayer.transform;
        data.PlayerStats = MainPlayer.GameStats;
        data.Settings = MainPlayer.Settings;
        data.Inventory = MainPlayer.GetComponent<InventoryManager>().Inventory;
        data.Loadout = MainPlayer.GetComponent<EquipmentManager>().Loadout;
        return data;
    }
}

[Serializable]
public class SaveData
{
    public PlayerData Player;
    //public 

}


[Serializable]
public class PlayerData
{
    public Transform PlayerTransform;
    public CharacterStats PlayerStats;
    public PlayerSettings Settings;
    public InventoryObj Inventory;
    public EquipmentLoadout Loadout;
}
