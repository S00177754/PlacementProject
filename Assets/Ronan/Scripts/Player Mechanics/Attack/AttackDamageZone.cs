using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageZone : MonoBehaviour
{
    public PlayerAttack player;
    public string ZoneName;
    private bool IsDealingDamage = false;

    public void SetPlayer(PlayerAttack playerAtk)
    {
        player = playerAtk;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        IsDealingDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyStatsScript enemy;
        if(other.gameObject.TryGetComponent<EnemyStatsScript>(out enemy))
        {
            if(!player.EnemiesToDamage.Contains(enemy))
            player.EnemiesToDamage.Add(enemy);
        }
    }

    


}
