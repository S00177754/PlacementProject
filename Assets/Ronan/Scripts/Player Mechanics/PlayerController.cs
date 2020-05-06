﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string PlayerName;
    public CharacterStats GameStats;
    public PlayerSettings Settings;
    public PlayerHUDController HUDController;
}

[Serializable]
public class PlayerSettings
{
    [Header("Camera Settings")]
    public bool InvertYAxis = true;
    public float CameraSensitivity = 2f;
}

[Serializable]
public class CharacterStats
{
    public int Level;

    [Header("Combat Stats")]
    public int MaxHealth = 100;
    public int Health = 100;
    public int MaxMP = 60;
    public int MP = 60;
}
