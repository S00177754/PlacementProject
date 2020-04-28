using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerSettings Settings;
}

[Serializable]
public class PlayerSettings
{
    [Header("Camera Settings")]
    public bool InvertYAxis = true;
    public float CameraSensitivity = 2f;
}
