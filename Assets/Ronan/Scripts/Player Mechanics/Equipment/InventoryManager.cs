using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObj Inventory;
    private Collider ItemOnGround;

    private void Start()
    {
       Inventory = Instantiate(Inventory); //Copy of object so i dont have to keep resetting values
    }

    public void PickUpItem()
    {
        if (ItemOnGround != null)
        {
            StartCoroutine(GetComponent<PlayerAttack>().FreezeMovementFor(1.9f, true, false));
            GetComponent<PlayerAnimator>().SetTrigger("GatherItem");

            CollectableItem item = ItemOnGround.GetComponent<CollectableItem>();
            Inventory.AddItem(item.Item, 1);
            Destroy(ItemOnGround.gameObject);

            GetComponent<PlayerController>().HUDController.SendNotification(ItemOnGround.GetComponent<CollectableItem>().Item.Name,null,Color.blue);
            GetComponent<PlayerController>().HUDController.HideItemNotification();
            GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Default);
            ItemOnGround = null;
        }
    }
     
    //Move to interaction manager script
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Pickup")
            {
                GetComponent<PlayerController>().HUDController.SetupItemNotification(other.GetComponent<CollectableItem>().Item.Name);
                GetComponent<InputManager>().buttonStates.SetState(WestButtonState.PickupItem);
                ItemOnGround = other;
            }
            else if(other.tag == "TravelPoint")
            {
                GetComponent<PlayerController>().HUDController.SetupNotification("Activate fast travel");
                GetComponent<InputManager>().buttonStates.SetState(WestButtonState.TravelPoint);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Pickup")
            {
                GetComponent<PlayerController>().HUDController.HideItemNotification();
                GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Default);
                ItemOnGround = null;
            }
        }
    }

}
