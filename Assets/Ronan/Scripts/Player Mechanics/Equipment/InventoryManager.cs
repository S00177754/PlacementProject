using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObj Inventory;
    private Collider ItemOnGround;
    [SerializeField]
    public QuestManager QuestManager;

    private void Start()
    {
        //Inventory = Instantiate(Inventory); //Copy of object so i dont have to keep resetting values
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

            if(QuestManager.ActiveMain.ActiveStep.GetType() == typeof(QuantityQuestStep))
            {
                QuantityQuestStep step = QuestManager.ActiveMain.ActiveStep as QuantityQuestStep;
                if (item.Item == step.targetObject)
                {
                    step.AddToCounter();
                }
            }

            //GetComponent<PlayerController>().HUDController.SendNotification(ItemOnGround.GetComponent<CollectableItem>().Item.Name,null,Color.blue);
            GetComponent<PlayerController>().HUDController.HideItemNotification();
            GetComponent<InputManager>().buttonStates.SetState(WestButtonState.Default);
            ItemOnGround = null;
        }
    }
     
    public void SetItemOnGround(Collider other)
    {
        ItemOnGround = other;
    }

    public void EquipWeapon(int weaponID)
    {
        if (Inventory != null)
        {

            InventorySlot slot = Inventory.Collection.Where(w => w.Item.ID == weaponID).SingleOrDefault();
            if (slot != null)
            {
                if (slot.Item.GetType() == typeof(WeaponObj))
                {
                    slot.Item.UseItem(PlayerController.Instance);
                }
            }
        }
    }

   
}
