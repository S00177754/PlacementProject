using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int CurrentSaveSlot = 0;

    public PlayerController MainPlayer;

    public List<PartyMember> Party;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }

        Party = new List<PartyMember>();
    }
}
