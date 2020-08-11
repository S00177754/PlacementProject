using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaubleSetterMenu : MonoBehaviour
{
    public BaubleObj EquipItem;

    public TMP_Text ButtonOneText;
    public TMP_Text ButtonTwoText;
    public TMP_Text ButtonThreeText;

    public void Setup(BaubleObj item)
    {
        EquipItem = item;
        EquipmentLoadout loadOut = PlayerController.Instance.GetComponent<EquipmentManager>().Loadout;
        ButtonOneText.text = loadOut.AccessorySlotOne != null ? loadOut.AccessorySlotOne.Name : "Empty";
        ButtonTwoText.text = loadOut.AccessorySlotTwo != null ? loadOut.AccessorySlotTwo.Name : "Empty";
        ButtonThreeText.text = loadOut.AccessorySlotThree != null ? loadOut.AccessorySlotThree.Name : "Empty";
    }

    public void EquipBaubleOne()
    {
        PlayerController.Instance.GetComponent<EquipmentManager>().EquipAccessory(EquipItem, 1);
    }

    public void EquipBaubleTwo()
    {
        PlayerController.Instance.GetComponent<EquipmentManager>().EquipAccessory(EquipItem, 2);
    }

    public void EquipBaubleThree()
    {
        PlayerController.Instance.GetComponent<EquipmentManager>().EquipAccessory(EquipItem, 3);
    }

}
