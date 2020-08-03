using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnConfirmController : MonoBehaviour
{
    public TMP_Text ConfirmationText;


    public void Confirm()
    {
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
