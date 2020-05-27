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
        if (player.MP < player.GameStats.MaxMP)
        {
            player.MP += RestoreAmount;

            if (player.MP > player.GameStats.MaxMP)
                player.MP = player.GameStats.MaxMP;

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

