using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Database", menuName = "Combat System/Enemy Database")]
[Serializable]
public class EnemyDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public EnemyInfo[] Info;

    public Dictionary<EnemyInfo, int> GetID = new Dictionary<EnemyInfo, int>();
    public Dictionary<int, EnemyInfo> GetInfo = new Dictionary<int, EnemyInfo>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<EnemyInfo, int>();
        GetInfo = new Dictionary<int, EnemyInfo>();

        for (int i = 0; i < Info.Length; i++)
        {
            Info[i].ID = i;
            GetID.Add(Info[i], i);
            GetInfo.Add(i, Info[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}
