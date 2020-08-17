using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int CurrentSaveSlot = 0;

    public PlayerController MainPlayer;

    public List<PartyMember> Party;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }

        LoadData();

        Party = new List<PartyMember>();
    }

    public SaveData GrabSaveData()
    {
        Debug.LogError("Grab Save Data");
        return null;
    }

    public void LoadData()
    {
        SaveData data;
        if (SaveUtility.TryLoadFromSlot(CurrentSaveSlot, out data))
        {
            Debug.Log(data.Player);
            PlayerController.Instance.transform.position = data.Player.PlayerPosition;
            PlayerController.Instance.transform.eulerAngles = data.Player.PlayerRotation;
            //TODO: Set loaded data to various objects
        }
        else
        {
            Debug.LogError("No Save File Detected");
        }
    }
}
