using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public List<AudioClip> BackgroundTracks;
    public List<string> BackgroundTrackNames;

    private AudioSource Source;

    public void PlayTrack(string TrackName,bool loop)
    {
        AudioClip clip = BackgroundTracks[BackgroundTrackNames.IndexOf(TrackName)];
        if(clip != null)
        {
            Source.clip = clip;
            Source.loop = loop;
            Source.Play();
        }
    }
}
