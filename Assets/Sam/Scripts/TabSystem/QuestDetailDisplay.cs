using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Class to display and fill scroll items for details pannel
public class QuestDetailDisplay : MonoBehaviour
{
    /**
     * DONE: Get questSteps 
     * TODO: Add questSteps to list
     * TODO: add items in queststeps to scrollview items
     * TODO: conditional formatting of complete and not complete
     * */

    QuestManager QuestManager;
    
    [Header("Required")]
    public GameObject StepListItemPrefab;

    //public TMP_Text QuestName;
    public TMP_Text QuestDescription;
    public TMP_Text StepDescription;


    public Quest CurrentQuest;
    public QuestStep ActiveStep;

    public Color CompletedStepColor;
    public Color ActiveStepColor;

    public QuestDetailDisplay()
    {
        if (CurrentQuest != null)
        {
            ActiveStep = CurrentQuest.ActiveStep;
            //Set colours, completed steps will be faded
            CompletedStepColor = Color.gray;
            CompletedStepColor.a = 0.9f;
            ActiveStepColor = Color.white;

            //QuestName.text = CurrentQuest.Name;
            QuestDescription.text = CurrentQuest.Description;

            //Add completed steps in order before adding final active step
            foreach (QuestStep step in CurrentQuest.CompletedSteps)
            {
                //***********
                Instantiate<GameObject>(StepListItemPrefab);            //NEEDS ATTENTION
                //***********
                if (StepListItemPrefab.TryGetComponent<TMP_Text>(out StepDescription))
                {
                    StepDescription.text = step.GetDescription();
                    StepDescription.color = CompletedStepColor;
                }
            }

            //Final instantiate for current step
            Instantiate(StepListItemPrefab);
            if (StepListItemPrefab.TryGetComponent<TMP_Text>(out StepDescription))
            {
                StepDescription.text = string.Format("<indent=5%> " + ActiveStep.GetDescription() + "</indent>");
                StepDescription.color = ActiveStepColor;
            }
        }
        else
            Debug.Log("QuestDetailDisplay CurrentQuest is null");
    }

    public QuestDetailDisplay(Quest currentQuest)
    {
        CurrentQuest = currentQuest;
    }

    private void Start()
    {
        QuestManager = FindObjectOfType<QuestManager>();
        
    }
    void Update()
    {
        
    }
}
