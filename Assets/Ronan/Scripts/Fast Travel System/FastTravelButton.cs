using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FastTravelButton : MonoBehaviour
{
    public TMP_Text ButtonText;
    private string LocationName;

    public void SetInfo(string locationName)
    {
        LocationName = locationName;
        ButtonText.text = locationName;
    }

    public void Teleport()
    {
        Time.timeScale = 1;
        TravelPoint.TeleportTo(LocationName);
        GameStateController.ResumePreviousState();
    }

}
