using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Exp Requirement Object", menuName = "PlayerData/Level Exp Requirement Object")]
public class LevelExpScriptableObj : ScriptableObject
{
    public List<LevelExpRequirement> LevellingInfo;
}

[Serializable]
public class LevelExpRequirement
{
    public int Level;
    public int ExpRequired;
    public int AbilityPoints;
    public int SkillPoints;
}
