using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    public Toggle InvertYAxis;

    [Header("Graphical")]
    public TMP_Dropdown QualityDropdown;
    public TMP_Dropdown ResolutionDropdown;
    public Toggle FullscreenToggle;
    Resolution[] resolutions;

    [Header("Volume")]
    public AudioMixer masterMixer;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public TMP_Text MasterValue;
    public TMP_Text MusicValue;
    public TMP_Text SFXValue;

    private void Start()
    {
        //Change to player prefs
        InvertYAxis.isOn = PlayerPrefs.GetString("yaxis") == "true" ? true : false;
        FullscreenToggle.isOn = PlayerPrefs.GetString("fullscreen") == "true" ? true : false;
        resolutions = Screen.resolutions;
        ResolutionSetup();
        SetupVolume();
        QualitySetup();
    }

    private void QualitySetup()
    {
        QualityDropdown.ClearOptions();
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));

        string[] qualityLevels = QualitySettings.names;
        foreach (var item in qualityLevels)
        {
            QualityDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }


        QualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    private void ResolutionSetup()
    {
        ResolutionDropdown.ClearOptions();

        List<string> resOptions = new List<string>();

        int resolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            ResolutionDropdown.options.Add(new TMP_Dropdown.OptionData() { text = string.Concat(resolutions[i].width, " X ", resolutions[i].height) });
        
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
            }
        }

        ResolutionDropdown.value = resolutionIndex;
        ResolutionDropdown.RefreshShownValue();
        SetResolution(resolutionIndex);
    }



    //****************** INPUT

    public void OnFullScreenToggle(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefs.SetString("fullscreen", value ? "true" : "false");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution chosen = resolutions[resolutionIndex];
        Screen.SetResolution(chosen.width, chosen.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", resolutionIndex );
    }



    public void OnToggleYAxisInvert(bool value)
    {
        Debug.Log("Y-Axis toggle: " + value);
        //PlayerOne.Settings.InvertYAxis = value;
        PlayerPrefs.SetString("yaxis", value ? "true" : "false");
    }

    //Sound
    public void SetMaster(float sliderValue)
    {
        SetMixer("MasterVol",sliderValue);
        PlayerPrefs.SetFloat("MasterVol", sliderValue);
        MasterValue.text = string.Concat(Mathf.RoundToInt((MasterSlider.value / MasterSlider.maxValue)*100),"%");
    }

    public void SetMusic(float sliderValue)
    {
        SetMixer("MusicVol", sliderValue);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
        MusicValue.text = string.Concat(Mathf.RoundToInt((MusicSlider.value / MusicSlider.maxValue)*100), "%");
    }

    public void SetSFX(float sliderValue)
    {
        SetMixer("SFXVol", sliderValue);
        PlayerPrefs.SetFloat("SFXVol", sliderValue);
        SFXValue.text = string.Concat(Mathf.RoundToInt((SFXSlider.value / SFXSlider.maxValue)*100), "%");
    }

    private void SetMixer(string mixer,float value)
    {
        masterMixer.SetFloat(mixer, Mathf.Log10(value) * 20);
    }

    private void SetupVolume()
    {
        SetMaster(PlayerPrefs.GetFloat("MasterVol"));
        SetMusic(PlayerPrefs.GetFloat("MusicVol"));
        SetSFX(PlayerPrefs.GetFloat("SFXVol"));

        MasterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
    }
}
