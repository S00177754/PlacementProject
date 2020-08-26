using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public TravelPoint ActivePoint;

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
                            ActivePoint = travelPoint;
                        }
                    }
                    break;

                case "NPC":
                    NPCDialogueTrigger ChattyNPC;
                    if(other.TryGetComponent(out ChattyNPC))
                    {
                        if(ChattyNPC.canTalk)
                        {
                            GetComponent<InputManager>().buttonStates.SetState(WestButtonState.NPCTalk);
                            GetComponent<PlayerController>().HUDController.SetupNotification(ChattyNPC.NPCName);
                            FindObjectOfType<DialogueManager>().SetActiveNPC(ChattyNPC.name);
                        }
                    }
                    break;


                case "Merchant":
                    GetComponent<PlayerController>().HUDController.SetupItemNotification("Shop");
                    GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Merchant);
                    MerchantUIController.Instance.SetMerchant(other.gameObject.GetComponent<MerchantComponent>());// Notbeing set
                    break;


                default:
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            switch (other.tag)
            {
                case "TravelPoint":
                    TravelPoint travelPoint;
                    if (other.TryGetComponent(out travelPoint))
                    {
                        if (travelPoint.TeleportUnlocked)
                        {
                            GetComponent<PlayerController>().HUDController.SetupNotification("Teleport Unlocked");
                        }
                    }
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
                    ActivePoint = null;
                    break;

                case "NPC":
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
