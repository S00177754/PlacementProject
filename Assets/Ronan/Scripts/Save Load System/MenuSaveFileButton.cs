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
        SlotName.text = $"Slot Name {SlotNumber}";
        //TODO: Read in header for slot save data and display last played date
        //LastPlayedText.text = $"Last Played: \n{}";
    }

    public void ButtonAction()
    {
        switch (Mode)
        {
            case MenuFileBtnMode.Overwrite:
                SaveManager.TitleMenuController.ShowConfirmBox(SlotNumber);
                break;

            default:
            case MenuFileBtnMode.Load:
                GameManager.CurrentSaveSlot = SlotNumber;
                SceneManager.LoadScene(1);
                break;

        }
    }
}
