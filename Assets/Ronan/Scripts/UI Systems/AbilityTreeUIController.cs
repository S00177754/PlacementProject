using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityTreeUIController : MonoBehaviour
{
    public List<TreeGroup> Trees;
    [Space(10f)]
    public int TreeIndex = 0;

    [Header("UI Components")]
    public TMP_Text FilterLabel;
    public TMP_Text AbilityDescription;

    //TODO: Hook up UI to controller

    private void Start()
    {
        UpdateTreeView();
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
    }
}

[Serializable]
public class TreeGroup
{
    public string TreeLabel;
    public GameObject TreeScrollView;
    public GameObject FirstNodeElement;
}
