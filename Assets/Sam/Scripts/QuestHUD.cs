using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestHUD : MonoBehaviour
{
    [SerializeField]
    QuestManager Manager;
    //Quest currentQuest;

    [SerializeField]
    Image questIcon;
    [SerializeField]
    TMP_Text questName;
    [SerializeField]
    TMP_Text questStep;
    
    void Start()
    {
        //Manager = Resources.Load<QuestManager>("ScriptableObjects/QuestManager");

        //Switch Betweeen Main and Side, create 'TrackedQuest' in Manager
        if (Manager.ActiveMain.isMainScenario)
            questIcon.sprite = Resources.Load<Sprite>("Images/TEMP_Main");
        else
            questIcon.sprite = Resources.Load<Sprite>("Images/TEMP_Side");

        questName.text = Manager.ActiveMain.Name;
        questStep.text = Manager.ActiveMain.ActiveStep.GetDescription();
    }

    // Update is called once per frame
    void Update()
    {
        AssignToHUD();
    }

    public void AssignToHUD()
    {
        //Switch Betweeen Main and Side, create 'TrackedQuest' in Manager
        if (Manager.ActiveMain.isMainScenario)
            questIcon = Resources.Load<Image>("Images/TEMP_Main");
        else
            questIcon = Resources.Load<Image>("Images/TEMP_Side");

        questName.text = Manager.ActiveMain.Name;
        questStep.text = Manager.ActiveMain.ActiveStep.GetDescription();
    }
}
