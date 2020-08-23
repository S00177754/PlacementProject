using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int CurrentSaveSlot = 0;

    public AudioMixer Mixer;

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

        
        SetMixer("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        SetMixer("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        SetMixer("SFXVol", PlayerPrefs.GetFloat("SFXVol"));

        LoadData();

        Party = new List<PartyMember>();

        
    }

    private void SetMixer(string mixer, float value)
    {
        Mixer.SetFloat(mixer, Mathf.Log10(value) * 20);
    }

    public SaveData GrabSaveData()
    {
        Debug.LogError("Grab Save Data");
        GetComponent<SaveLoad>().Save();
        return null;
    }

    public void LoadData()
    {
        if(CurrentSaveSlot < 1 && CurrentSaveSlot > 4)
        {
            Debug.LogError("No Slot Selected");
        }
        else
        {
            GetComponent<SaveLoad>().Load();
        }
    }
}
