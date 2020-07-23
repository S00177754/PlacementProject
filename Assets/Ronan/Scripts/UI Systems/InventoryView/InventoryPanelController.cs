using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
    [Header("Required")]
    public bool ItemListActive = false;
    public ScrollRect Scroller;
    public RectTransform ScrollViewContent;
    public GameObject ItemPanelPrefab;
    public InventoryManager PlayerInventory;
    public GameObject ItemPanelMenu;

    [Header("Item Sprites")] //Have Static sprites set for panel?
    public Sprite HPItemSprite;
    public Sprite MPItemSprite;
    public Sprite WeaponItemSprite;



    private void Start()
    {
         
    }

    private void Update()
    {
        if (ItemListActive)
        {

        }
    }


    public void ClearList()
    {
        ItemListActive = false;
        for (int i = 0; i < ScrollViewContent.childCount; i++)
        {
            Destroy(ScrollViewContent.GetChild(i).gameObject);
        }
        ItemPanelMenu.SetActive(false);
    }

    public void GenerateList()
    {
        ClearList();

        for (int i = 0; i < PlayerInventory.Inventory.Collection.Count; i++)
        {
            GameObject panel = Instantiate(ItemPanelPrefab, ScrollViewContent);
            //panel.GetComponent<InventoryItemPanel>().SetDetails(PlayerInventory.Inventory.Collection[i], this);
        }

        if(ScrollViewContent.childCount > 0)
        UIHelper.SelectedObjectSet(ScrollViewContent.GetChild(0).gameObject);
        ItemListActive = true;
    }


}


   
     
