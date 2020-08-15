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
                    TravelPoint travelPoint;
                    if (other.TryGetComponent(out travelPoint))
                    {
                        if (!travelPoint.TeleportUnlocked)
                        {
                            GetComponent<PlayerController>().HUDController.SetupNotification(travelPoint.LocationName);
                            GetComponent<InputManager>().buttonStates.SetState(WestButtonState.TravelPoint);
                        }
                    }
                    break;

                case "Merchant":
                    GetComponent<PlayerController>().HUDController.SetupItemNotification("Shop");
                    GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Merchant);
                    MerchantUIController.Instance.SetMerchant(other.gameObject.GetComponent<MerchantComponent>());
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

                case "Merchant":
                    GetComponent<PlayerController>().HUDController.HideItemNotification();
                    GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Default);
                    MerchantUIController.Instance.SetMerchant(null);
                    break;

                default:
                    break;
            }
        }
    }

}
