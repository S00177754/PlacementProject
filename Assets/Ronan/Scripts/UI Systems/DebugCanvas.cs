using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugCanvas : MonoBehaviour
{
    public GameObject Player;

    public TMP_Text GameStateText;
    public TMP_Text PlayerMapText;
    public TMP_Text AttackComboText;
    public TMP_Text ComboTimerText;

    void Update()
    {
        GameStateText.text = "Game State: " + GameStateController.gameState.ToString();
        PlayerMapText.text = "Action Map: " + Player.GetComponent<PlayerInput>().currentActionMap.name;
        AttackComboText.text = "Combo Count: " + Player.GetComponent<PlayerAttack>().comboCounter;
        ComboTimerText.text = "Combo Timer: " + Player.GetComponent<PlayerAttack>().ComboTimer;
    }
}
