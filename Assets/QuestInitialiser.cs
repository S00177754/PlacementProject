using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitialiser : MonoBehaviour
{
    [SerializeField]
    QuestManager QuestManager;
    [SerializeField]
    int QuestNumber;
    public bool resetAll;

    // Start is called before the first frame update
    void Start()
    {
        /************************
         * REMOVE AFTER TESTING *
         * **********************/
        if(resetAll)
            ResetAllQuests();

        QuestManager.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResetAllQuests()
    {
        //QuestManager.Initialise();
        foreach (Quest q in QuestManager.MainScenarioQuests)
        {
            q.isComplete = false;
            q.isFound = false;
            q.Initialise();
            q.CompletedList.Clear();
            if (q.StepsList.Count > 0)
            {
                foreach (QuestStep s in q.StepsList)
                {
                    s.isComplete = false;
                }
            }
        }

        AssignNthQuest(QuestNumber);
    }

    public void AssignNthQuest(int index)
    {
        QuestManager.ActiveMain = QuestManager.MainScenarioQuests[index - 1];
        QuestManager.ActiveMain.isFound = true;
        QuestManager.ActiveMain.ActiveStep = QuestManager.ActiveMain.StepsList[index - 1];
        QuestManager.FoundMSQuests.Add(QuestManager.ActiveMain);
    }
}
