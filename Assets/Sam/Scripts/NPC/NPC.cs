using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC Objects", order = 0)]
public class NPC : ScriptableObject
{
    int ID;
    [SerializeField]
    public string Name;
    string Description;
    Vector3 Location;
    [SerializeField]
    string DefaultSpeach;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
