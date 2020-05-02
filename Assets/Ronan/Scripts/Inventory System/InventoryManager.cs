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

}
