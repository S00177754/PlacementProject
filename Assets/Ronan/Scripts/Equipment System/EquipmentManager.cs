using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public EquipmentLoadout Loadout;

    private void Start()
    {
        GetComponent<PlayerAttack>().ComboAttackCount = GetAttackDetails().PrimaryAtackPattern.Count;
    }


    public void EquipWeapon(GameObject weaponPrefab)
    {
        WeaponInfo wi;

        if (!weaponPrefab.TryGetComponent<WeaponInfo>(out wi))
        {
            Debug.LogError("GameObject does not have a WeaponInfo component.");
        }
        else
        {
            GameObject go = Instantiate(weaponPrefab);
            Loadout.EquippedWeapon = go.GetComponent<WeaponInfo>();
            GetComponent<PlayerAttack>().WeaponSheathed = true;
            go.SetActive(false);
        }
    }

    public void UnEquipWeapon()
    {
        Loadout.EquippedWeapon = null;
    }


    public void EquipAccessory(BaubleObj bauble,int accessorySlot)
    {
        //TODO: Ensure unequip is working

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

}

[Serializable]
public class EquipmentLoadout
{
    public WeaponInfo EquippedWeapon;

    public BaubleObj AccessorySlotOne;
    public BaubleObj AccessorySlotTwo; //Lock with ability tree
    public BaubleObj AccessorySlotThree; //Lock with ability tree

    public float GetAttackBonus()
    {
        return AccessorySlotOne.GetAttackBonus() + AccessorySlotTwo.GetAttackBonus() + AccessorySlotThree.GetAttackBonus();
    }

    public float GetDefenseBonus()
    {
        return AccessorySlotOne.GetDefenseBonus() + AccessorySlotTwo.GetDefenseBonus() + AccessorySlotThree.GetDefenseBonus();
    }

    public float GetHealthBonus()
    {
        return AccessorySlotOne.GetHealthBonus() + AccessorySlotTwo.GetHealthBonus() + AccessorySlotThree.GetHealthBonus();
    }

    public float GetMPBonus()
    {
        return AccessorySlotOne.GetMPBonus() + AccessorySlotTwo.GetMPBonus() + AccessorySlotThree.GetMPBonus();
    }
}
