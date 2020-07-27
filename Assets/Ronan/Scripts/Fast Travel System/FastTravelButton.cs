using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FastTravelButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public TMP_Text ButtonText;
    private string LocationName;
    private FastTravelMapViewer mapView;

    public void SetInfo(string locationName,FastTravelMapViewer mapViewer)
    {
        mapView = mapViewer;
        LocationName = locationName;
        ButtonText.text = locationName;
    }

    public void Teleport()
    {
        Time.timeScale = 1;
        TravelPoint.TeleportTo(LocationName);
        GameStateController.ResumePreviousState();
    }


    public void ShowLocation()
    {
        Vector3 loc = TravelPoint.GetWorldPosition(LocationName);
        if(loc != new Vector3(-1, -1, -1))
        {
            mapView.MoveIcon(loc);
            return;
        }
        mapView.SetIconVisible(false);
    }

    //Controller OnSelect
    public void OnSelect(BaseEventData eventData)
    {
        ShowLocation();
    }

    //Mouse Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowLocation();
    }
}
