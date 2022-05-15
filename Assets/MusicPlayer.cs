using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] tracks;

    private void Start()
    {
        PlayMusic();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayMusic();
        }
    }

    private void PlayMusic()
    {
        int trackToPlay = UnityEngine.Random.Range(0, tracks.Length);
        audioSource.clip = (tracks[trackToPlay]);
        audioSource.Play();
    }
}
