using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Object", menuName = "Inventory System/Item/Weapon")]
public class WeaponObj : ItemObj
{
    public GameObject WeaponPrefab;

    public void Awake()
    {
        Type = ItemType.Weapon;
    }

    public override bool UseItem(PlayerController player)
    {
        if (player.GetComponent<EquipmentManager>().Loadout.EquippedWeapon != WeaponPrefab.GetComponent<WeaponInfo>())
        {
            player.GetComponent<EquipmentManager>().EquipWeapon(WeaponPrefab);
        }
        else
        {
            player.GetComponent<EquipmentManager>().UnEquipWeapon();
        }

        return true;
    }

    public override bool UseItem(PartyMember member)
    {
        base.UseItem(member);
        return false;
    }

    public override string GetItemDetailText()
    {
        WeaponInfo wi = WeaponPrefab.GetComponent<WeaponInfo>();

        string description = string.Concat(Enum.GetName(typeof(WeaponType), wi.weaponType), " that deals ", Enum.GetName(typeof(AttackType), wi.attackType).ToLower(), " type damage.");

        description = string.Concat(description, "\n\n", Description);

        return description;
    }
}