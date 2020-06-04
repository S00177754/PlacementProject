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

    public override string GetItemDetailText()
    {
        string description = string.Concat("Heals ", HealAmount, " HP to ");

        if (AffectParty)
        {
            description = string.Concat(description,"the whole party.");
        }
        else
        {
            description = string.Concat(description, "the player.");
        }

        description = string.Concat(description, "\n\n",Description);

        return description;
    }

}
