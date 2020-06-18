using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapGrid mapGrid;

    private void Start()
    {
        InstantiateCells();
    }

    public void InstantiateCells()
    {
        foreach (var cell in mapGrid.cells)
        {
            Instantiate(cell.Terrain);
            cell.Unload();
        }
    }
}
