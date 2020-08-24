using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestTestingReset : MonoBehaviour
{
    [SerializeField]
    QuestManager QuestManager;
    // Start is called before the first frame update
    void Start()
    {
        //foreach (Quest q in QuestManager.MainScenarioQuests)
        //{
        //    q.isComplete = false;
        //    foreach (QuestStep s in q.StepsList)
        //    {
        //        s.isComplete = false;
        //    }
        //}

        //Quest Starting = QuestManager.MainScenarioQuests.First();
        //Starting.ActiveStep = Starting.StepsList.First();
    }

    public void ResetAllQuests()
    {
        foreach (Quest q in QuestManager.MainScenarioQuests)
        {
            q.isComplete = false;
            q.CompletedList.Clear();
            foreach (QuestStep s in q.StepsList)
            {
                s.isComplete = false;
            }
        }

        QuestManager.ActiveMain = QuestManager.MainScenarioQuests.First();
        QuestManager.ActiveMain.ActiveStep = QuestManager.ActiveMain.StepsList.First();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
