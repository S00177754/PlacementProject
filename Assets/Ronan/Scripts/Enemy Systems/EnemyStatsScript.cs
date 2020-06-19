using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    public EnemyInfo Info;
    public int Health = 20;
    //public int MaxHealth = 20;

    public void ApplyDamage(int value)
    {
        Health -= value;
        //Debug.Log("DAMAGE");

        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
