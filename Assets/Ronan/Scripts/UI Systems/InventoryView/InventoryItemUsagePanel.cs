using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemUsagePanel : MonoBehaviour
{
    public InventoryItemPanel ActivatedBy;
    public GameObject FirstButton;
    public ItemSettingRadialMenu RadialSetter;

    public void Activate(InventoryItemPanel activatedBy)
    {
        ActivatedBy = activatedBy;
        UIHelper.SelectedObjectSet(this.gameObject);
    }

    public void UseItem()
    {
        if(ActivatedBy.Item is HealthPotionObj ||
           ActivatedBy.Item is HealthPotionObj)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Party.Count == 0)
            {

                if (ActivatedBy.Item.UseItem(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer))
                {
                    ActivatedBy.InventoryPanel.PlayerInventory.Inventory.RemoveItem(ActivatedBy.Item, 1);
                    ActivatedBy.InventoryPanel.GenerateList();

                    //Animation call here
                    ReturnFocus();
                    GameStateController.ResumePreviousState();
                }
            }
        }
        else if(ActivatedBy.Item is WeaponObj)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Party.Count == 0)
            {

                if (ActivatedBy.Item.UseItem(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer))
                {
                    ReturnFocus();
                }
            }
        }
    }

    public void QuickWheel()
    {
        UIHelper.SelectedObjectSet(null);
        RadialSetter.gameObject.SetActive(true);
        RadialSetter.ItemToSet = ActivatedBy.Item;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().buttonStates.SetState(RightJoystickState.RadialMenu);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().buttonStates.SetState(SouthButtonState.RadialMenu);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().SetRadialMenu(RadialSetter, InputManager.RadialMenuState.ItemSetter);
        (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>().ActiveRadialMenu as ItemSettingRadialMenu).Activate(this);
    }


    private void ReturnFocus()
    {
        UIHelper.SelectedObjectSet(ActivatedBy.gameObject);
        RadialSetter.gameObject.SetActive(false);
    }

    public void FocusOn()
    {
        UIHelper.SelectedObjectSet(FirstButton);
    }

}
