using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerInitialise : MonoBehaviour
{
    [SerializeField]
    QuestManager QuestManager;
    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Initialise();
    }
}
