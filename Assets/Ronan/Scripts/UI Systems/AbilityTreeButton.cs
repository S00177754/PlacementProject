using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTreeButton : MonoBehaviour
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
        if(AttatchedNode.AbilityPointCost > PlayerController.Instance.GameStats.AbilityPoints || AttatchedNode.NodeUnlocked)
        {
            return;
        }

        PlayerController.Instance.GameStats.AbilityPoints -= AttatchedNode.AbilityPointCost;
        AttatchedNode.NodeUnlocked = true;

        SetColor();
        Debug.Log(string.Concat("Remaining Ability Points: ", PlayerController.Instance.GameStats.AbilityPoints));
    }

    private void SetColor()
    {
        GetComponent<Image>().color = AttatchedNode.NodeUnlocked == true ? UnlockedColor : BaseColor;
    }
}
