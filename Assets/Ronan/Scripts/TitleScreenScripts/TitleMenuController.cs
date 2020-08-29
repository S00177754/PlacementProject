using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TitleMenuState { Root, NewGame, LoadGame, Settings}

public class TitleMenuController : MonoBehaviour
{
    public GameObject RootMenu;
    public GameObject SettingMenu;
    public GameObject SlotMenu;
    public GameObject OverwriteConfirmation;

    public Button RootButton;
    public Toggle SettingToggle;
    public Button SlotButton;
    public Button OverwriteButton;

    public TitleSaveFileManager SaveManager;
    private int slot = 0;

    public AudioMixer Mixer;

    // Start is called before the first frame update
    void Start()
    {
        Mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        Mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        Mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));

        ReturnToMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        MenuSaveFileButton.Mode = MenuFileBtnMode.Overwrite;
        ChangeToMenu(TitleMenuState.NewGame);
        Debug.Log(string.Concat("NewGame - ",MenuSaveFileButton.Mode));
    }

    public void LoadGame()
    {
        MenuSaveFileButton.Mode = MenuFileBtnMode.Load;
        ChangeToMenu(TitleMenuState.LoadGame);
        Debug.Log(string.Concat("Load - ",MenuSaveFileButton.Mode));
    }

    public void Settings()
    {
        ChangeToMenu(TitleMenuState.Settings);
    }

    public void ReturnToMenu()
    {
        ChangeToMenu(TitleMenuState.Root);
    }

    public void Exit()
    {
        Application.Quit();
    }



    private void ChangeToMenu(TitleMenuState state)
    {
        switch (state)
        {
            case TitleMenuState.Root:
                RootMenu.SetActive(true);
                SettingMenu.SetActive(false);
                SlotMenu.SetActive(false);
                OverwriteConfirmation.SetActive(false);
                UIHelper.SelectedObjectSet(RootButton.gameObject);
                break;

            case TitleMenuState.Settings:
                RootMenu.SetActive(false);
                SettingMenu.SetActive(true);
                SlotMenu.SetActive(false);
                OverwriteConfirmation.SetActive(false);
                UIHelper.SelectedObjectSet(SettingMenu.gameObject);
                break;

            case TitleMenuState.NewGame:
                RootMenu.SetActive(false);
                SettingMenu.SetActive(false);
                SlotMenu.SetActive(true);
                OverwriteConfirmation.SetActive(false);
                UIHelper.SelectedObjectSet(SlotButton.gameObject);
                break;

            case TitleMenuState.LoadGame:
                RootMenu.SetActive(false);
                SettingMenu.SetActive(false);
                SlotMenu.SetActive(true);
                OverwriteConfirmation.SetActive(false);
                UIHelper.SelectedObjectSet(SlotButton.gameObject);
                break;
        }
    }



    public void ShowConfirmBox(int slotNum)
    {
        UIHelper.SelectedObjectSet(OverwriteConfirmation.gameObject);
        OverwriteConfirmation.SetActive(true);
        slot = slotNum;
    }

    public void ConfirmOverwrite()
    {
        //SceneTransitionController.Instance.FadeOut();
        SaveManager.CreateNewGame(slot);
        GameManager.CurrentSaveSlot = slot;
        SceneManagerHelper.TransitionToScene(1);
        Debug.Log(string.Concat("I've overwritten slot ", slot));
    }

    public void CancelConfirmation()
    {
        OverwriteConfirmation.SetActive(false);
        ReturnToMenu();
    }
}
