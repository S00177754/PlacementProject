using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController MainPlayer;

    public List<PartyMember> Party;

    private void Start()
    {
        Party = new List<PartyMember>();
    }
}
