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

    private void OnTriggerEnter(Collider other)
    {
        //
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
