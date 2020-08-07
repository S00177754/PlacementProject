using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Info", menuName = "Combat System/Enemy Info")]
[Serializable]
public class EnemyInfo : ScriptableObject
{
    public int ID;
    public GameObject Prefab;

    [Header("Enemy Attributes")]
    public int Health;
    public int Attack;
    public float AttackRange;
    public float AttackCooldown;
    public int Experience;

    [Header("Speed")]
    public float PatrolSpeed;
    public float ChaseSpeed;

    [Header("Treasure Table")]
    public List<TreasureTableLine> TreasureTable;
}

[Serializable]
public class TreasureTableLine
{
    public ItemObj ItemDrop;
    public int DropChancePercentage;
}

public static class MyExtensions
{
    public static ItemObj GetItemDrop(this List<TreasureTableLine> treasureTable)
    {
        int totalChance = 0;
        treasureTable.ForEach(t => totalChance += t.DropChancePercentage);

        int item = UnityEngine.Random.Range(0, totalChance + 1);

        int previousAmount = 0;
        int dropPercentageTotal = 0;

        for (int i = 0; i < treasureTable.Count; i++)
        {
            dropPercentageTotal += treasureTable[i].DropChancePercentage;

            if(item > previousAmount && item <= dropPercentageTotal)
            {
                return treasureTable[i].ItemDrop;
            }

            previousAmount = dropPercentageTotal;
        }

        return null;
    }
}
