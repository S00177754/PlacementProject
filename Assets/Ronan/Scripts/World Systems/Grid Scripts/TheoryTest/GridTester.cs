using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTester : MonoBehaviour
{
    private WorldGrid grid;
    public bool Debug = false;

    void Start()
    {
        grid = new WorldGrid(4, 2,20);
        
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (Debug && grid != null)
        {
            grid.DrawGridZones();
        }
    }
}
