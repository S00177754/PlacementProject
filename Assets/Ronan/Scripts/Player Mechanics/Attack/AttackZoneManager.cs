using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneManager : MonoBehaviour
{
    public PlayerAttack Player;
    private Dictionary<string, AttackDamageZone> Zones = new Dictionary<string, AttackDamageZone>();
    
    public void GetAllAttackPatterns()
    {
        AttackDamageZone[] components = GetComponentsInChildren<AttackDamageZone>(true);
        for (int i = 0; i < components.Length; i++)
        {
            Zones.Add(components[i].ZoneName, components[i]);
            components[i].SetPlayer(Player);
        }
    }

    public void ActivateZone(string name)
    {
        AttackDamageZone zone;
        if (Zones.TryGetValue(name, out zone))
        {
            zone.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Zone Manager: Zone does not exist.");
        }
    }

    public void DisableZones()
    {
        foreach (KeyValuePair<string, AttackDamageZone> item in Zones)
        {
            item.Value.gameObject.SetActive(false);
        }
    }
}
