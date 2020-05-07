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
            CollectableItem item = ItemOnGround.GetComponent<CollectableItem>();
            Inventory.AddItem(item.Item, 1);
            Destroy(ItemOnGround.gameObject);
            GetComponent<PlayerController>().HUDController.HideItemNotification();
            GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Default);
            ItemOnGround = null;
        }
    }

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
