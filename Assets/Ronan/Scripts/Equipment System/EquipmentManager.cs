using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public EquipmentLoadout Loadout;

    private void Start()
    {
        HideWeapon();
        GetComponent<PlayerAttack>().ComboAttackCount = GetAttackDetails().PrimaryAtackPattern.Count;
        GetComponent<PlayerAttack>().AttachPoints.AttatchTo(AttachPoint.RightHand, Loadout.EquippedWeapon);
    }


    public void EquipWeapon(GameObject weaponPrefab)
    {
        WeaponInfo wi;

        if (!weaponPrefab.TryGetComponent(out wi))
        {
            Debug.LogError("GameObject does not have a WeaponInfo component.");
        }
        else
        {
            Destroy(GetComponent<PlayerAttack>().Equipment.Loadout.EquippedWeapon.gameObject);

            GameObject go = Instantiate(weaponPrefab);
            Loadout.EquippedWeapon = go.GetComponent<WeaponInfo>();
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
        Loadout.EquippedWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    public void HideWeapon()
    {
        Loadout.EquippedWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public bool IsWeaponMeshActive()
    {
        return Loadout.EquippedWeapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled;
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
