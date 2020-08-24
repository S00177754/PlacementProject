using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    [SerializeField]
    string LocationName;
    [SerializeField]
    QuestManager QuestManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
            if (QuestManager.ActiveMain.ActiveStep.ID == LocationName)
            {
                QuestManager.ActiveMain.ActiveStep.isComplete = true;
                QuestManager.ActiveMain.GoToNextStep();
            }   
    }
}
