using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;

//Class to display and fill scroll items for details pannel
public class QuestDetailDisplay : MonoBehaviour
{
    /**
     * TODO
     * Get questSteps
     * Add questSteps to list
     * add items in queststeps to scrollview items
     * conditional formatting of complete and not complete
     * */

    QuestManager QuestManager;
    
    [Header("Required")]
    public GameObject StepListItemPrefab;

    public Text QuestName;
    public Text QuestDescription;
    public Text StepDescription;

    public Quest CurrentQuest;
    public QuestStep ActiveStep;

    public Color CompletedStepColor;
    public Color ActiveStepColor;

    public QuestDetailDisplay()
    {

    }

    public QuestDetailDisplay(Quest currentQuest)
    {
        CurrentQuest = currentQuest;
    }

    void Start()
    {
        //QuestManager = 
        if (CurrentQuest != null)
        {
            ActiveStep = CurrentQuest.ActiveStep;
            //Set colours, completed steps will be faded
            CompletedStepColor = new Color(255, 255, 255, 0.75f);
            ActiveStepColor = new Color(255, 255, 255, 1);

            QuestName.text = CurrentQuest.Name;
            QuestDescription.text = CurrentQuest.Description;

            //Add completed steps in order before adding final active step
            foreach (QuestStep step in CurrentQuest.CompletedSteps)
            {
                //***********
                Instantiate<GameObject>(StepListItemPrefab);            //NEEDS ATTENTION
                //***********
                if(StepListItemPrefab.TryGetComponent<Text>(out StepDescription))
                {
                    StepDescription.text = step.Description;
                    StepDescription.color = CompletedStepColor;
                }
            }

            //Final instantiate for current step
            Instantiate(StepListItemPrefab);
            if(StepListItemPrefab.TryGetComponent<Text>(out StepDescription))
            {
                StepDescription.text = ActiveStep.Description;
                StepDescription.color = ActiveStepColor;
            }
        }
        else
            Debug.Log("QuestDetailDisplay CurrentQuest is null");
    }

    void Update()
    {
        
    }
}
