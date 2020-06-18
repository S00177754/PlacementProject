using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid 
{
    private int GridWidth;
    private int GridHeight;
    private float CellSize;
    private int[,] GridArray;

    public WorldGrid(int width,int height,float cellSize)
    {
        GridWidth = width;
        GridHeight = height;
        CellSize = cellSize;

        GridArray = new int[width, height];

        PrintArray();
    }

    public void PrintArray()
    {
        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int y = 0; y < GridArray.GetLength(1); y++)
            {
                //Debug.Log(string.Concat(x, " , ", y));
            }
        }
    }

    public void DrawGridZones()
    {
        for (int x = 0; x < GridArray.GetLength(0); x++)
        {
            for (int y = 0; y < GridArray.GetLength(1); y++)
            {
                if (x == 0 && y == 0)
                    Gizmos.color = Color.green;
                else if (x == 3 && y == 1)
                    Gizmos.color = Color.blue;
                else
                    Gizmos.color = Color.red;

                Gizmos.DrawWireCube(GetWorldPosition(x, y) + new Vector3(CellSize/2,0,CellSize/2), new Vector3(CellSize, CellSize, CellSize));
                
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x,0, y) * CellSize;
    }

    
}

public class WorldCell
{
    private GameObject Terrain;
    private List<GameObject> Objects;
}
