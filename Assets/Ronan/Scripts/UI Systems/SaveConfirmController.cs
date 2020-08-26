using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveConfirmController : MonoBehaviour
{
    public TMP_Text ConfirmationText;


    public void Confirm()
    {
        //SaveUtility.SaveToSlot(GameManager.Instance.GrabSaveData(), GameManager.CurrentSaveSlot);
        GameManager.Instance.GetComponent<SaveLoad>().Save();
        
        gameObject.SetActive(false);
    }
}
