using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bauble Object", menuName = "Inventory System/Item/Bauble")]
public class BaubleObj : ItemObj
{
    public float AttackBonus;
    public float DefenseBonus;
    public float MaxHealthBonus;
    public float MaxMPBonus;

    public void Awake()
    {
        Type = ItemType.Bauble;
    }

    public float GetAttackBonus()
    {
        return AttackBonus;
    }

    public float GetDefenseBonus()
    {
        return DefenseBonus;
    }

    public float GetHealthBonus()
    {
        return MaxHealthBonus;
    }

    public float GetMPBonus()
    {
        return MaxMPBonus;
    }

    public override bool UseItem(PlayerController player)
    {

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
        string description = "An accessory item which provides the following bonuses:";
        description = string.Concat(description,AttackBonus > 0 ? $"\nAttack Bonus: {AttackBonus}" : "");
        description = string.Concat(description, DefenseBonus > 0 ? $"\nDefense Bonus: {DefenseBonus}" : "");
        description = string.Concat(description, MaxHealthBonus > 0 ? $"\nHP Bonus: {MaxHealthBonus}" : "");
        description = string.Concat(description, MaxMPBonus > 0 ? $"\nMP Bonus: {MaxMPBonus}" : "");

        description = string.Concat(description, "\n\n", Description);

        return description;
    }
}