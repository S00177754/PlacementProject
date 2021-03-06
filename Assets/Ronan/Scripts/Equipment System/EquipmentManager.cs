﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public EquipmentLoadout Loadout;

    private void Start()
    {
        HideWeapon();
        if(GetAttackDetails() != null)
        {
            GetComponent<PlayerAttack>().ComboAttackCount = GetAttackDetails().PrimaryAtackPattern.Count;
        }
        GetComponent<PlayerAttack>().AttachPoints.AttatchTo(AttachPoint.RightHand, Loadout.EquippedWeapon);
    }


    public void EquipWeapon(GameObject weaponPrefab,int ID)
    {
        WeaponInfo wi;

        if (!weaponPrefab.TryGetComponent(out wi))
        {
            Debug.LogError("GameObject does not have a WeaponInfo component.");
        }
        else
        {
            if(GetComponent<PlayerAttack>().Equipment.Loadout.EquippedWeapon != null)
            Destroy(GetComponent<PlayerAttack>().Equipment.Loadout.EquippedWeapon.gameObject);

            GameObject go = Instantiate(weaponPrefab);
            Loadout.EquippedWeapon = go.GetComponent<WeaponInfo>();
            Loadout.EquippedWeapon.ItemID = ID;
            GetComponent<PlayerAttack>().AttachPoints.AttatchTo(AttachPoint.RightHand, Loadout.EquippedWeapon);
            GetComponent<PlayerAttack>().ComboAttackCount = GetAttackDetails().PrimaryAtackPattern.Count;
        }
    }

    public bool CheckLoadout(ItemObj item)
    {
        switch (item.Type)
        {
            case ItemType.Weapon:
                if(Loadout.EquippedWeapon.AttackDetails == (item as WeaponObj).WeaponPrefab.GetComponent<WeaponInfo>().AttackDetails)
                {
                    return true;
                }
                break;

            case ItemType.Bauble:
                if (Loadout.AccessorySlotOne == (item as BaubleObj) || Loadout.AccessorySlotTwo == (item as BaubleObj) || Loadout.AccessorySlotThree == (item as BaubleObj) )
                {
                    return true;
                }
                break;                
        }
                return false;
    }

    public int GetBaubleSlot(BaubleObj bauble)
    {
        if (CheckLoadout(bauble))
        { 
            if (Loadout.AccessorySlotOne == bauble)
            {
                return 1;
            }
            else if (Loadout.AccessorySlotTwo == bauble)
            {
                return 2;
            }
            else if (Loadout.AccessorySlotThree == bauble)
            {
                return 3;
            }
        }

        return -1;
    }

    public void UnEquipWeapon()
    {
        Loadout.EquippedWeapon = null;
    }


    public void EquipAccessory(BaubleObj bauble,int accessorySlot)
    {
        UnequipAccessory(accessorySlot);

        switch (accessorySlot)
        {
            case 1:
                Loadout.AccessorySlotOne = bauble;
                break;

            case 2:
                Loadout.AccessorySlotTwo = bauble;
                break;

            case 3:
                Loadout.AccessorySlotThree = bauble;
                break;

            default:
                Debug.LogError("Invalid accesory slot selection.");
                break;
        }
    }


    public void UnequipAccessory(int accessorySlot)
    {
        switch (accessorySlot)
        {
            case 1:
                Loadout.AccessorySlotOne = null;
                break;

            case 2:
                Loadout.AccessorySlotTwo = null;
                break;

            case 3:
                Loadout.AccessorySlotThree = null;
                break;

            default:
                Debug.LogError("Invalid accesory slot selection.");
                break;
        }
    }


    public WeaponAttackDetailsObj GetAttackDetails()
    {
        if (Loadout.EquippedWeapon != null)
        {
            return Loadout.EquippedWeapon.AttackDetails;
        }
        else return null;

    }


    public void ShowWeapon()
    {
        if (Loadout.EquippedWeapon != null)
            Loadout.EquippedWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    public void HideWeapon()
    {
        if(Loadout.EquippedWeapon != null)
        Loadout.EquippedWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public bool IsWeaponMeshActive()
    {
        if (Loadout.EquippedWeapon != null)
        {
            return Loadout.EquippedWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
        }
        else return false;
    }

}

[Serializable]
public class EquipmentLoadout
{
    public WeaponInfo EquippedWeapon;

    public BaubleObj AccessorySlotOne;
    public BaubleObj AccessorySlotTwo; //Lock with ability tree
    public BaubleObj AccessorySlotThree; //Lock with ability tree

    public int GetAttackBonus()
    {
        int total = 0;

        if(AccessorySlotOne != null)
        {
            total += AccessorySlotOne.GetAttackBonus();
        }

        if(AccessorySlotTwo != null)
        {
            total += AccessorySlotTwo.GetAttackBonus();
        }

        if(AccessorySlotThree != null)
        {
            total += AccessorySlotThree.GetAttackBonus();
        }

        return total;
    }

    public int GetDefenseBonus()
    {
        int total = 0;

        if (AccessorySlotOne != null)
        {
            total += AccessorySlotOne.GetDefenseBonus();
        }

        if (AccessorySlotTwo != null)
        {
            total += AccessorySlotTwo.GetDefenseBonus();
        }

        if (AccessorySlotThree != null)
        {
            total += AccessorySlotThree.GetDefenseBonus();
        }

        return total;
    }

    public int GetHealthBonus()
    {
        int total = 0;

        if (AccessorySlotOne != null)
        {
            total += AccessorySlotOne.GetHealthBonus();
        }

        if (AccessorySlotTwo != null)
        {
            total += AccessorySlotTwo.GetHealthBonus();
        }

        if (AccessorySlotThree != null)
        {
            total += AccessorySlotThree.GetHealthBonus();
        }

        return total;
    }

    public int GetMPBonus()
    {
        int total = 0;

        if (AccessorySlotOne != null)
        {
            total += AccessorySlotOne.GetMPBonus();
        }

        if (AccessorySlotTwo != null)
        {
            total += AccessorySlotTwo.GetMPBonus();
        }

        if (AccessorySlotThree != null)
        {
            total += AccessorySlotThree.GetMPBonus();
        }

        return total;
    }
}
