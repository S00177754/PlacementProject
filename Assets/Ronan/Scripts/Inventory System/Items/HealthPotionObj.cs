using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion Object", menuName = "Inventory System/Item/Potions/Health")]
public class HealthPotionObj : ItemObj
{
    public int HealAmount;
    public bool AffectParty;

    public void Awake()
    {
        Type = ItemType.Potion;
    }

    public override bool UseItem(PlayerController player)
    {
        if(player.Health < player.GameStats.MaxHealth)
        {
            player.Health += HealAmount;

            if (player.Health > player.GameStats.MaxHealth)
                player.Health = player.GameStats.MaxHealth;

            return true;
        }


        base.UseItem(player);
        return false;
    }

    public override bool UseItem(PartyMember member)
    {
        base.UseItem(member);
        return false;
    }

}
