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
}
