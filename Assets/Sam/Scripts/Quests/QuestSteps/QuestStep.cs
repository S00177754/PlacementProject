using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStep", menuName = "Quest System/QuestStep", order = 0)]
public class QuestStep : ScriptableObject {
    
    protected int ID;
    [SerializeField]
    [TextArea(10, 15)]
    protected string Description;
    public bool isComplete;
}

//public class LocationQuestStep : QuestStep
//{
   
//}

//public class NPCQuestStep : QuestStep
//{
    
//}

//public class QuantityQuestStep : QuestStep
//{
    
//}


//public class MultiQuantityQuestStep : QuestStep
//{
    
//}
