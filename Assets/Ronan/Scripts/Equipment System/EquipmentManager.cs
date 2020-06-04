using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Weapon")]
    public WeaponInfo ActiveWeapon;

    private void Start()
    {
        GetComponent<PlayerAttack>().ComboAttackCount = GetAttackDetails().PrimaryAtackPattern.Count;
    }

    //[Header("Accessories")]
    public BaubleObj AccessorySlotOne;
    public BaubleObj AccessorySlotTwo; //Lock with ability tree
    public BaubleObj AccessorySlotThree; //Lock with ability tree

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
            ActiveWeapon = go.GetComponent<WeaponInfo>();
            GetComponent<PlayerAttack>().WeaponSheathed = true;
            GetComponent<PlayerAttack>().SheathWeapon();
        }
    }

    public void UnEquipWeapon()
    {
        ActiveWeapon = null;
    }

    public WeaponAttackDetailsObj GetAttackDetails()
    {
        if (ActiveWeapon != null)
        {
            return ActiveWeapon.AttackDetails;
        }
        else return null;

    }

}
