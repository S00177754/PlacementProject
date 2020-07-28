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
            StartCoroutine(DeathLogic());
        }
    }

    public IEnumerator DeathLogic()
    {
        //TODO Death animation

        
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Level Geometry"));
        
        //TODO Instantiate ground item at hit.point then get random item from treasue table
        if(hit.collider.gameObject != null)
        {

        }

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
