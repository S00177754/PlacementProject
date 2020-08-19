using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance; 

    [Header("Sources")]
    public AudioSource SourceOne;
    public AudioSource SourceTwo;

    [Header("Details")]
    public bool IsSourceOneActive;
    [Range(0,3)]
    public int CrossfadeDuration = 1;
    public string CurrentTrackName = "placeholder606";
    public string PreviousTrackName;
    public string DefaultTrack;

    [Header("Track List")]
    public List<AudioObj> BackgroundTracks;

    IEnumerator musicTransition;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayTrack(string trackName)
    {
        AudioObj audioObj = BackgroundTracks.Where(bg => bg.TrackName == trackName).SingleOrDefault();
        if (audioObj == null)
            return;

        //if (audioObj.Track.name == IsSourceOneActive ? SourceOne.clip.name : SourceTwo.clip.name) //If already playing
        //    return;

        if (IsSourceOneActive)
        {
            if (audioObj.Track == SourceOne.clip)
                return;
        }
        else
        {
            if (audioObj.Track == SourceTwo.clip)
                return;
        }

        if (musicTransition != null)
            StopCoroutine(musicTransition);
        

        if (CurrentTrackName != PreviousTrackName)
        {
            PreviousTrackName = CurrentTrackName;
            CurrentTrackName = trackName;
        }

        if (IsSourceOneActive)
        {
            SourceTwo.clip = audioObj.Track;
            SourceTwo.Play();
        }
        else
        {
            SourceOne.clip = audioObj.Track;
            SourceOne.Play();
        }

        musicTransition = FadeIntoNext(CrossfadeDuration * 10);
        StartCoroutine(musicTransition);
    }

    public void PlayLastTrack()
    {
        if (!string.IsNullOrWhiteSpace(PreviousTrackName) || PreviousTrackName != "placeholder606")
            PlayTrack(PreviousTrackName);
        else
            PlayTrack(DefaultTrack);
    }

    private AudioSource GetActiveSource()
    {
        return IsSourceOneActive ? SourceOne : SourceTwo;
    }

    private IEnumerator FadeIntoNext(int duration)
    {
        for (int i = 0; i < duration + 1; i++)
        {
            SourceOne.volume = IsSourceOneActive ? (duration - i) * (1f / duration) : (0 + i) * (1f / duration);
            SourceTwo.volume = !IsSourceOneActive ? (duration - i) * (1f / duration) : (0 + i) * (1f / duration);

            yield return new WaitForSecondsRealtime(0.1f);
        }

            if (IsSourceOneActive)
            { SourceOne.Stop(); }
            else 
            { SourceTwo.Stop(); }

            IsSourceOneActive = !IsSourceOneActive;

    }

}

[Serializable]
public class AudioObj
{
    public AudioClip Track;
    public string TrackName;
}
