using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MP Potion Object", menuName = "Inventory System/Item/Potions/MP")]
public class MPPotionObj : ItemObj
{
    public int RestoreAmount;
    public bool AffectParty;

    public void Awake()
    {
        Type = ItemType.Potion;
    }

    public override bool UseItem(PlayerController player)
    {
        if (player.MP < player.MaxMP)
        {
            player.MP += RestoreAmount;

            if (player.MP > player.MaxMP)
                player.MP = player.MaxMP;

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
        string description = string.Concat("Restores ", RestoreAmount, " MP to ");

        if (AffectParty)
        {
            description = string.Concat(description, "the whole party.");
        }
        else
        {
            description = string.Concat(description, "the player.");
        }

        description = string.Concat(description, "\n\n", Description);

        return description;
    }
}

