using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    public EnemyInfo Info;
    public int Health = 20;

    private void Start()
    {
        Health = Info.Health;
    }

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
