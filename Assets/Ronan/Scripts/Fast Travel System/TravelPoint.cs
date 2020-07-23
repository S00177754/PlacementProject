using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPoint : MonoBehaviour
{
    static public Dictionary<string,TravelPoint> FastTravelPoints = new Dictionary<string, TravelPoint>();

    public string LocationName;
    public bool TeleportUnlocked = false;
    public Transform TeleportationPoint;
    public GameObject Particles;

    private void Start()
    {
        if (!TeleportUnlocked)
        {
            Particles.SetActive(false);
        }
        else
        {
            Particles.SetActive(true);
        }

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
            Particles.SetActive(true);
        }
    }

    private void UnlockTeleportPoint()
    {
        TeleportUnlocked = true;
    }

    static public void TeleportTo(string locationName)
    {
        if (FastTravelPoints.ContainsKey(locationName))
        {
            TravelPoint point = FastTravelPoints[locationName];

            PlayerController.Instance.GetComponent<CharacterController>().enabled = false;
            PlayerController.Instance.transform.SetPositionAndRotation(point.TeleportationPoint.position,point.TeleportationPoint.rotation);
            PlayerController.Instance.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            Debug.LogError(string.Concat("Fast Travel Point: ", locationName, " does not exist."));
        }
    }
}
