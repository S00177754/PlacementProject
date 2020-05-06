using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    [Header("Required")]
    public RectTransform ScrollViewContent;
    public GameObject ItemPanelPrefab;
    public InventoryObj PlayerInventory;

    [Header("Item Sprites")] //Have Static sprites set for panel?
    public Sprite HPItemSprite;
    public Sprite MPItemSprite;
    public Sprite WeaponItemSprite;

    public void ClearList()
    {
        for (int i = 0; i < ScrollViewContent.childCount; i++)
        {
            Destroy(ScrollViewContent.GetChild(i).gameObject);
        }
    }

    public void GenerateList()
    {
        ClearList();

        for (int i = 0; i < PlayerInventory.Collection.Count; i++)
        {
            GameObject panel = Instantiate(ItemPanelPrefab, ScrollViewContent);
            panel.GetComponent<InventoryItemPanel>().SetDetails(PlayerInventory.Collection[i], this);
        }

        if(ScrollViewContent.childCount > 0)
        UIHelper.SelectedObjectSet(ScrollViewContent.GetChild(0).gameObject);
    }
}
