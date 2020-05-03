using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    public PlayerController PlayerOne; //Temporary setup, need to check which player paused.
    public Toggle InvertYAxis;

    private void Start()
    {
        InvertYAxis.isOn = PlayerOne.Settings.InvertYAxis;
    }

    public void OnToggleYAxisInvert(bool value)
    {
        Debug.Log("Y-Axis toggle: " + value);
        PlayerOne.Settings.InvertYAxis = value;
    }
}
