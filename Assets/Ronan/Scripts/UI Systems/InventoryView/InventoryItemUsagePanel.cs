using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItemUsagePanel : MonoBehaviour
{
    static public InventoryItemUsagePanel Instance;

    public InventoryItemPanel ActivatedBy;
    public GameObject FirstButton;
    public ItemSettingRadialMenu RadialSetter;
    public TMP_Text UseButtonText;
    public BaubleSetterMenu BaubleSetMenu;

    private void Start()
    {
        Instance = this;
    }

    public void Activate(InventoryItemPanel activatedBy)
    {
        ActivatedBy = activatedBy;
        PlayerController.Instance.GetComponent<InputManager>().buttonStates.SetState(EastButtonState.ItemUsagePanel);
        UIHelper.SelectedObjectSet(this.gameObject);

        if(ActivatedBy.Item is WeaponObj || activatedBy.Item is BaubleObj)
        {
            if(PlayerController.Instance.GetComponent<EquipmentManager>().CheckLoadout(ActivatedBy.Item))
            {
                UseButtonText.text = "Unequip";
            }
            else
            {
                UseButtonText.text = "Equip";
            }

        }
        else if(!(ActivatedBy.Item is TreasureObj))
        {
            UseButtonText.text = "Use Item";
        }
    }

    public void UseItem()
    {
        if(ActivatedBy.Item is HealthPotionObj || ActivatedBy.Item is HealthPotionObj)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Party.Count == 0)
            {

                if (ActivatedBy.Item.UseItem(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer))
                {
                    ActivatedBy.InventoryPanel.TargetInventory.Inventory.RemoveItem(ActivatedBy.Item, 1);
                    ActivatedBy.InventoryPanel.FilterList();

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
        else if (ActivatedBy.Item is BaubleObj)
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Party.Count == 0)
            {
                if (PlayerController.Instance.GetComponent<EquipmentManager>().CheckLoadout(ActivatedBy.Item))
                {
                    PlayerController.Instance.GetComponent<EquipmentManager>().UnequipAccessory(PlayerController.Instance.GetComponent<EquipmentManager>().GetBaubleSlot(ActivatedBy.Item as BaubleObj));
                }
                else
                {
                    BaubleSetMenu.gameObject.SetActive(true);
                    BaubleSetMenu.Setup(ActivatedBy.Item as BaubleObj);
                }
            }
        }


    }

    public void ActivateItemRadialSetter()
    {
        UIHelper.SelectedObjectSet(null); //Removes focus from item context menu

        //Activation of radial setter
        ActivatedBy.InventoryPanel.ShowRadialMenu();
        RadialSetter.gameObject.SetActive(true);
        RadialSetter.ItemToSet = ActivatedBy.Item;

        InputManager inputPlayer = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>();

        //Change over of input states in the input manager
        inputPlayer.buttonStates.SetState(RightJoystickState.RadialMenu);
        inputPlayer.buttonStates.SetState(SouthButtonState.RadialMenu);
        inputPlayer.SetRadialMenu(RadialSetter, InputManager.RadialMenuState.ItemSetter);

        //Setup of Radial Setter
        (inputPlayer.ActiveRadialMenu as ItemSettingRadialMenu).SetInventoryItemUsagePanel(this);
        (inputPlayer.ActiveRadialMenu as ItemSettingRadialMenu).Startup();
    }  //Need to handle change back once used and ensure item is set correctly

    public void CloseItemRadialSetter()
    {
        ReturnFocus();
        ActivatedBy.InventoryPanel.ShowInventoryList();
        RadialSetter.CloseMenu();

        InputManager inputPlayer = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().MainPlayer.GetComponent<InputManager>();
        inputPlayer.buttonStates.SetState(RightJoystickState.Default);
        inputPlayer.buttonStates.SetState(SouthButtonState.Default);
        inputPlayer.buttonStates.SetState(SouthButtonState.Default);
        inputPlayer.buttonStates.SetState(EastButtonState.Default);
        inputPlayer.SetRadialMenu(null, InputManager.RadialMenuState.None);
    }

    public void CloseUsagePanel()
    {
        ReturnFocus();
        gameObject.SetActive(false);

        PlayerController.Instance.GetComponent<InputManager>().buttonStates.SetState(EastButtonState.Default);
    }

    private void ReturnFocus()
    {
        UIHelper.SelectedObjectSet(ActivatedBy.gameObject);
        //RadialSetter.gameObject.SetActive(false);
    }

    public void FocusOn()
    {
        UIHelper.SelectedObjectSet(FirstButton);
    }

}
