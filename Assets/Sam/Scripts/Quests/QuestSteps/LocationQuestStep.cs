using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationQuestStep", menuName = "Quest System/Quest Step/LocationQuestStep")]
public class LocationQuestStep : QuestStep
{
    [Header("Required")]
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;
    [SerializeField]
    public string ColiderTag;

    [Header("Type Specific")]
    [SerializeField]
    public Transform StepLocation;
    [SerializeField]
    public float TriggerRange;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ColiderTag))
            SetComplete();
        ParentQuest.GoToNextStep();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override string GetName()
    {
        return Name;
    }

    public override string GetDescription()
    {
        return Description;
    }
}
