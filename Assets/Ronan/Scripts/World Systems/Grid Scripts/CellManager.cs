using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CellManager : EditorWindow
{
    private MapGrid mapGrid;

    [MenuItem("Custom/Map Cell Manager")] //1
    public static void ShowWindow() //2
    {
        GetWindow<CellManager>("Cell Manager"); //3
    }

    private void OnGUI()
    {

        mapGrid = (MapGrid)EditorGUILayout.ObjectField("Map Grid Data",mapGrid, typeof(MapGrid), false, GUILayout.Width(300f), GUILayout.Height(20f));

        if (mapGrid != null)
        {

            if (mapGrid.cells.Count > 0)
            {
                foreach (var cell in mapGrid.cells)
                {
                    if (cell != null)
                    {

                        GUILayout.BeginHorizontal();
                        GUILayout.Label(string.Concat("Cell: ", cell.GridIndex.ToString(), " | Zone: ",cell.ZoneName, " | Entities: ",cell.Entities.Count," | Entity Cap: ",cell.EntityCap));
                        //if (GUILayout.Button("Load",GUILayout.Height(20f),GUILayout.Width(60f)))
                        //{
                        //    Load(cell);
                        //}
                        //if (GUILayout.Button("Unload", GUILayout.Height(20f), GUILayout.Width(60f)))
                        //{
                        //    Unload(cell);
                        //}
                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
    }

    void Load(CellData cell)
    {
        if (cell.Terrain != null)
        {
            cell.Load();
        }
        else
        {
            Debug.LogError("Terrain not set in cell.");
        }

    }

    void Unload(CellData cell)
    {
        if (cell.Terrain != null)
        {
            cell.Unload();
        }
        else
        {
            Debug.LogError("Terrain not set in cell.");
        }

    }



}
