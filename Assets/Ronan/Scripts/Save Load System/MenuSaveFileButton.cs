using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuFileBtnMode { Overwrite, Load }

public class MenuSaveFileButton : MonoBehaviour
{
    static TitleSaveFileManager SaveManager;
    public static MenuFileBtnMode Mode;

    public TitleSaveFileManager saveManager;
    public int SlotNumber;

    [Header("UI Components")]
    public TMP_Text SlotName;
    public Image SlotImage;
    public TMP_Text LastPlayedText;


    private void Awake()
    {
        if(saveManager != null)
        {
            SaveManager = saveManager;
        }
    }

    private void Start()
    {
        saveManager = SaveManager;
        Setup();
    }

    private void Setup()
    {
        SlotName.text = SlotNumber.ToString();
        //TODO: Read in header for slot save data and display last played date
        //LastPlayedText.text = $"Last Played: \n{}";
        SaveSlotData data;
        SaveUtility.TryLoadSlotInfo(SlotNumber,out data);
        if(data != null)
        {
            LastPlayedText.text = string.Concat("Progress: ", Math.Round(data.PercentageComplete, 2), "%  \nLast Played: ", data.LastPlayed);
        }
    }

    public void ButtonAction()
    {
        switch (Mode)
        {
            case MenuFileBtnMode.Overwrite:
                if (SaveUtility.CheckForFile(SlotNumber))
                {
                    SaveManager.TitleMenuController.ShowConfirmBox(SlotNumber);
                }
                else
                {
                    SaveManager.CreateNewGame(SlotNumber);
                    GameManager.CurrentSaveSlot = SlotNumber;
                    SceneManagerHelper.TransitionToScene(1);
                    Debug.Log(string.Concat("I've created a new game in slot ", SlotNumber));
                }
                break;

            default:
            case MenuFileBtnMode.Load:
                if (SaveUtility.CheckForFile(SlotNumber))
                {
                    GameManager.CurrentSaveSlot = SlotNumber;
                    SceneManager.LoadScene(1);
                }
                else
                {
                    SaveManager.CreateNewGame(SlotNumber);
                    GameManager.CurrentSaveSlot = SlotNumber;
                    SceneManagerHelper.TransitionToScene(1);
                    Debug.Log(string.Concat("I've created a new game in slot ", SlotNumber));
                }
                
                break;

        }
    }
}
