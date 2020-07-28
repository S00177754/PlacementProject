using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public RectTransform MapImage;
    public RectTransform LocationIcon;
    public Vector2 maxWorldDiemnsions;

    private void Update()
    {
        MoveIcon(PlayerController.Instance.transform.position);
    }

    public void MoveIcon(Vector3 worldPosition)
    {
        float percentageX = worldPosition.x / maxWorldDiemnsions.x;
        float percentageY = worldPosition.z / maxWorldDiemnsions.y;
        LocationIcon.anchoredPosition = new Vector3(percentageX * MapImage.rect.width, percentageY * MapImage.rect.height, 0);
    }
}
