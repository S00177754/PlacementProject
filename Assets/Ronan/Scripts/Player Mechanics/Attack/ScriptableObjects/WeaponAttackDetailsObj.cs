using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Attack Details Object", menuName = "Combat System/Weapon Attack Details")]
public class WeaponAttackDetailsObj : ScriptableObject 
{
    public WeaponType WeaponType;
    public PlayerStatTypes StatModifier;

    public List<AttackInfoObj> PrimaryAtackPattern;
    public AttackInfoObj ChargeAttack;
}
