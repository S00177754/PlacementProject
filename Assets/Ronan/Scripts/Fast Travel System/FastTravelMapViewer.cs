using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastTravelMapViewer : MonoBehaviour
{
    public RectTransform MapImage;
    public RectTransform LocationIcon;
    public Vector2 maxWorldDiemnsions;
    public bool PlayerCorrection = false;

    public void MoveIcon(Vector3 worldPosition)
    {
        float percentageX = worldPosition.x / maxWorldDiemnsions.x;
        float percentageY = worldPosition.z / maxWorldDiemnsions.y;

        SetIconVisible(true);
        LocationIcon.anchoredPosition = new Vector3(percentageX * MapImage.rect.width + (PlayerCorrection ? 63 : 0), percentageY * MapImage.rect.height + 10,0);
    }

    public void SetIconVisible(bool show)
    {
        LocationIcon.GetComponent<Image>().enabled = show;
    }
}
