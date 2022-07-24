using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG;

public class MusicControler : MonoBehaviour
{
    [SerializeField] AudioSource _audio;

    [SerializeField] Toggle _toggle;
   
    public void SwitchMusic()
    {
        if(_toggle.isOn)
        {
            _audio.Play();
        }
        else
        {
            _audio.Pause();
        }
    }

    public void FadeMusic()
    {
        _audio.volume = 0.02f;
    }
    public void RiseMusic()
    {
        _audio.volume = 1;
    }
}
