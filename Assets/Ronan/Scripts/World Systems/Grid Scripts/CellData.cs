using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Cell Data Object", menuName = "World Systems/Cell Data")]
public class CellData : ScriptableObject
{
    public Vector2 GridIndex;

    [Header("Cell Settings")]
    public bool Loaded = true;
    public string ZoneName;
    public int EntityCap = 10;

    [Header("Cell Objects")]
    public GameObject Terrain;
    public List<Entity> Entities;

    

    public void Load()
    {
        Loaded = true;
        Terrain.SetActive(true);
        Entities.ForEach(e => e.gameObject.SetActive(true));
    }

    public void Unload()
    {
        Loaded = false;
        Terrain.SetActive(false);
        Entities.ForEach(e => e.gameObject.SetActive(false));
    }
}
