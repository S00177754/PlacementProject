using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravelMenuController : MonoBehaviour
{
    public GameObject Player;
    public GameObject TravelPointList;
    public RectTransform Content;
    public GameObject ButtonPrefab;

    public void GenerateList()
    {
        ClearList();

        foreach (var point in TravelPoint.FastTravelPoints)
        {
            if (point.Value.TeleportUnlocked)
            {
                GameObject button = Instantiate(ButtonPrefab, Content);
                button.GetComponent<FastTravelButton>().SetInfo(point.Value.LocationName);
            }
        }

        if (Content.childCount > 0)
            UIHelper.SelectedObjectSet(Content.GetChild(0).gameObject);

    }

    public void ClearList()
    {
        for (int i = 0; i < Content.childCount; i++)
        {
            Destroy(Content.GetChild(i).gameObject);
        }
    }

}
