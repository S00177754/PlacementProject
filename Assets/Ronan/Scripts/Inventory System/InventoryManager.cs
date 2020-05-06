using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObj Inventory;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CollectableItem item = null;

        if(hit.gameObject.TryGetComponent(out item))
        {
            Inventory.AddItem(item.Item, 1);
            hit.collider.enabled = false; //Stops collision from firing twice before object gets destroyed
            Destroy(hit.gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pickup")
        {
            //Need to refactor for states in input manager, west button to pick up items if in trigger zone?
            //Could be a good idea to introduce states for each input to allow for wider variety of actions
        }
    }

}
