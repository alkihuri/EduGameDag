﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBinder : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] string _currentAudioPath;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("CURRENT_AUDIO_KEY","Intro");
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SyncAudio(); 
    }

    private void SyncAudio()
    {
        _currentAudioPath = PlayerPrefs.GetString("CURRENT_AUDIO_KEY").Replace("-", ""); ;
        try
        {
            if (_audioSource.clip.name != _currentAudioPath)
            {
                _audioSource.clip = AudioGetter(_currentAudioPath);
                _audioSource.Play();
            }
        }
        catch   
        {
            Debug.Log("НЕТ СОКРЫТИЮ! ДАААА! ");
        }
    }

    AudioClip AudioGetter(string currentAudio)
    {
        var path = "Audio/Find/" + currentAudio;
        return  Resources.Load<AudioClip>(path) as AudioClip;
    }
}