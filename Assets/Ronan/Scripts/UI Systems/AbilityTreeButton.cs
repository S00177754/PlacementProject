using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityTreeButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public AbilityTreeNode AttatchedNode;
    public Color BaseColor;
    public Color UnlockedColor;

    private void Start()
    {
        SetColor();
    }

    public void PurchaseSkill()
    {
        if(AttatchedNode.AbilityPointCost > PlayerController.Instance.GameStats.AbilityPoints || AttatchedNode.NodeUnlocked || AttatchedNode.PreviousNodesUnlocked() == false)
        {
            return;
        }

        PlayerController.Instance.GameStats.AbilityPoints -= AttatchedNode.AbilityPointCost;
        AttatchedNode.NodeUnlocked = true;
        AbilityTreeUIController.Description.text = AttatchedNode.GetDescription();
        AbilityTreeUIController.RemainingPoints.text = string.Concat("Remaining Ability Points: ", PlayerController.Instance.GameStats.AbilityPoints);
        SetColor();
        Debug.Log(string.Concat("Remaining Ability Points: ", PlayerController.Instance.GameStats.AbilityPoints));
    }

    private void SetColor()
    {
        GetComponent<Image>().color = AttatchedNode.NodeUnlocked == true ? UnlockedColor : BaseColor;
    }



    public void OnSelect(BaseEventData eventData)
    {
        AbilityTreeUIController.Description.text = AttatchedNode.GetDescription();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AbilityTreeUIController.Description.text = AttatchedNode.GetDescription();
    }
}
