using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationQuestStep", menuName = "Quest System/LocationQuestStep")]
public class LocationQuestStep : QuestStep
{
    //Assign to empty
    public Vector3 StepLocation;
    [SerializeField]
    public float TriggerRange;
    [SerializeField]
    public string Description;
    [SerializeField]
    public string Name;

    private void OnTriggerEnter(Collider other)
    {
        //
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
