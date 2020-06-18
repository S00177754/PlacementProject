using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Map Grid Object", menuName = "World Systems/Map Grid")]
public class MapGrid : ScriptableObject
{
    //Cell Data
    public List<CellData> cells;

    //Map Settings
    //public int mapWidth;
    //public int mapHeight;

    public float cellSize;
    
    
}
