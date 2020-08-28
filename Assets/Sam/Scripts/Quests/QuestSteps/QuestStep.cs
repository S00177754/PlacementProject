using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnLifeTime { Permanent, Step }

public class QuestStep : ScriptableObject 
{    
    public string ID;
    public bool isComplete;
    public Quest ParentQuest;

    [Header("Spawn Object Options")]
    public bool SpawnObjectAtQuestStart;
    public List<QuestStepSpawnable> Spawnables;


    public virtual void Initialise()
    {
        for (int i = 0; i < Spawnables.Count; i++)
        {
            if (Spawnables[i].Prefab != null && SpawnObjectAtQuestStart)
            {
                Spawnables[i].Spawned = Instantiate(Spawnables[i].Prefab);
                Spawnables[i].Spawned.transform.position = new Vector3(Spawnables[i].SpawnLocation.x, Spawnables[i].SpawnLocation.y, Spawnables[i].SpawnLocation.z);
                Debug.Log(string.Concat("Spawned Quest Obejct: ", ID));
            }
        }
    }

    public virtual void SetComplete()
    {
        isComplete = true;

        for (int i = 0; i < Spawnables.Count; i++)
        {
            if (Spawnables[i].Spawned != null && Spawnables[i].LifeTime == SpawnLifeTime.Step)
            {
                Destroy(Spawnables[i].Spawned);
            }
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

[Serializable]
public class QuestStepSpawnable
{
    public SpawnLifeTime LifeTime;
    public GameObject Prefab;
    public Vector3 SpawnLocation;
    [NonSerialized]
    public GameObject Spawned;
}