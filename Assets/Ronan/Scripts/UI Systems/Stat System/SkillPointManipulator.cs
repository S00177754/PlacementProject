using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointManipulator : MonoBehaviour
{
    public enum SkillType { Strength,Dexterity,Magic,Vitality,Defence}

    [Header("External")]
    public SkillPointPanelController Controller;
    public Button Subtract;
    public Button Addition;

    [Header("Mainpulator Details")]
    public SkillType Type;
    public int PointsToAdd = 0;
    public TMP_Text PointText;

    public void AddPoint()
    {
        //if (Controller.TotalPointAmount >= Controller.GetPointTotal())
        if (Controller.GetPointTotal() >= PlayerController.Instance.GameStats.SkillPoints)
        {
            Controller.InteractableCheck();
            return;
        }

        PointsToAdd++;

        UpdatePointValue();
        Controller.InteractableCheck();
    }

    public void RemovePoint()
    {
        if (PointsToAdd <= 0)
            return;

        PointsToAdd--;

        UpdatePointValue();
        Controller.InteractableCheck();
    }

    public void UpdatePointValue()
    {
        PointText.text = PointsToAdd.ToString();
    }
}
