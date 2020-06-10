using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathNode : MonoBehaviour
{
    public EnemyPathNode NextNode;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            EnemyBehaviour enemy;
            if (other.TryGetComponent(out enemy))
            {
                enemy.NextEnemyNode = NextNode;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.2f);
        

        Gizmos.color = Color.blue;

        if(NextNode != null)
        Gizmos.DrawLine(transform.position,NextNode.transform.position);
    }
}
