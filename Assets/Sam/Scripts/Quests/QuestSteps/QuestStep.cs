using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestStep : ScriptableObject 
{    
    public string ID;
    public bool isComplete;
    public Quest ParentQuest;

    [Header("Spawn Object Options")]
    public bool SpawnObjectAtQuestStart;
    public Vector3 ItemSpawnLocation;
    public GameObject SpawnPrefab;


    public virtual void Initialise()
    {
        if(SpawnPrefab != null && SpawnObjectAtQuestStart)
        {
            GameObject go = Instantiate(SpawnPrefab);
            go.transform.position = new Vector3(ItemSpawnLocation.x,ItemSpawnLocation.y,ItemSpawnLocation.z);
            Debug.Log(string.Concat("Spawned Quest Obejct: ",ID));
        }
    }

    public virtual string GetName()
    {
        return string.Empty;
    }

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}