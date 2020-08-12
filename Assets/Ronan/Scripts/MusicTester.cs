using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTester : MonoBehaviour
{
    public void PlayTrack(string name)
    {
        PlayerController.Instance.MusicManager.PlayTrack(name);
    }
}
