using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public List<EnemyPathNode> PathNodes;
    public List<EnemyBehaviour> PathUsers;

    public void AssignIds()
    {
        for (int i = 0; i < PathUsers.Count; i++)
        {
            PathUsers[i].PathID = new PathUserID(this, i);
        }
    }
}

public class PathUserID
{
    public EnemyPath Path;
    public int ID;

    public PathUserID(EnemyPath path,int id)
    {
        Path = path;
        ID = id;
    }
}
