using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPoint : MonoBehaviour
{
    static public Dictionary<string,TravelPoint> FastTravelPoints = new Dictionary<string, TravelPoint>();

    public string LocationName;
    public bool TeleportUnlocked = false;
    public Transform TeleportationPoint;

    private void Start()
    {
        if (!FastTravelPoints.ContainsKey(LocationName))
        {
            FastTravelPoints.Add(LocationName, this);
        }
        else
        {
            Debug.LogError(string.Concat("Fast Travel Point: ", LocationName, " already exists."));
        }
    }

    public void Interact()
    {
        if (!TeleportUnlocked)
        {
            TeleportUnlocked = true;
        }
        else
        {
            //Show UI for teleportation List
        }
    }

    private void UnlockTeleportPoint()
    {
        TeleportUnlocked = true;
    }

    private void TeleportTo(string locationName,GameObject obj)
    {
        if (FastTravelPoints.ContainsKey(locationName))
        {
            TravelPoint point = FastTravelPoints[locationName];

            if (point.TeleportUnlocked)
            {
                obj.transform.position = point.TeleportationPoint.position;
            }
            else
            {
                Debug.Log(string.Concat("Travel point: ",point.LocationName," is not yet unlocked."));
            }

        }
        else
        {
            Debug.LogError(string.Concat("Fast Travel Point: ", locationName, " does not exist."));
        }
    }
}
