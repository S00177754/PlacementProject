using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityTreeUIController : MonoBehaviour
{
    public List<TreeGroup> Trees;
    [Space(10f)]
    public int TreeIndex = 0;

    [Header("UI Components")]
    public TMP_Text FilterLabel;
    public TMP_Text AbilityDescription;
    public TMP_Text AbilityPointsText;
    public static TMP_Text Description;
    public static TMP_Text RemainingPoints;

    public GameObject LeftFilterButton;
    public GameObject RightFilterButton;

    //TODO: Hook up UI to controller

    private void Start()
    {
        Description = AbilityDescription;
        RemainingPoints = AbilityPointsText;
        UpdateTreeView();
    }

    public void Setup()
    {
        AbilityPointsText.text = string.Concat("Remaining Ability Points: ", PlayerController.Instance.GameStats.AbilityPoints);
    }

    public void NextIndex()
    {
        TreeIndex++;

        if(TreeIndex >= Trees.Count)
        {
            TreeIndex = 0;
        }

        UpdateTreeView();
    }

    public void PreviousIndex()
    {
        TreeIndex--;

        if (TreeIndex < 0)
        {
            TreeIndex = Trees.Count - 1;
        }

        UpdateTreeView();
    }

    private void UpdateTreeView()
    {
        Trees.ForEach(t => t.TreeScrollView.SetActive(false));
        Trees[TreeIndex].TreeScrollView.SetActive(true);
        FilterLabel.text = Trees[TreeIndex].TreeLabel;
        UIHelper.SelectedObjectSet(Trees[TreeIndex].FirstNodeElement);

        Navigation navL = LeftFilterButton.GetComponent<Button>().navigation;
        navL.selectOnDown = Trees[TreeIndex].FirstNodeElement.GetComponent<Button>();
        LeftFilterButton.GetComponent<Button>().navigation = navL;

        Navigation navR = RightFilterButton.GetComponent<Button>().navigation;
        navR.selectOnDown = Trees[TreeIndex].FirstNodeElement.GetComponent<Button>();
        RightFilterButton.GetComponent<Button>().navigation = navR;
    }
}

[Serializable]
public class TreeGroup
{
    public string TreeLabel;
    public GameObject TreeScrollView;
    public GameObject FirstNodeElement;
}
