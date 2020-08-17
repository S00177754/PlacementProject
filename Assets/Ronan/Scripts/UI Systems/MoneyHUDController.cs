using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyHUDController : MonoBehaviour
{
    static public MoneyHUDController Instance;
    public TMP_Text Txt_Money;

    private void Awake()
    {
        Instance = this;
    }

    public void SetAmount(int money)
    {
        Txt_Money.text = money.ToString();
    }
}
