using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
   //Move to interaction manager script
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            switch (other.tag)
            {
                case "Pickup":
                    GetComponent<PlayerController>().HUDController.SetupItemNotification(other.GetComponent<CollectableItem>().Item.Name);
                    GetComponent<InputManager>().buttonStates.SetState(WestButtonState.PickupItem);
                    GetComponent<InventoryManager>().SetItemOnGround(other);
                    break;

                case "TravelPoint":
                    GetComponent<PlayerController>().HUDController.SetupNotification("Activate fast travel");
                    GetComponent<InputManager>().buttonStates.SetState(WestButtonState.TravelPoint);
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            switch (other.tag)
            {
                case "Pickup":
                    GetComponent<PlayerController>().HUDController.HideItemNotification();
                    GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Default);
                    GetComponent<InventoryManager>().SetItemOnGround(null);
                    break;

                case "TravelPoint":
                    GetComponent<PlayerController>().HUDController.HideItemNotification();
                    GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Default);
                    break;

                default:
                    break;
            }
        }
    }

}
