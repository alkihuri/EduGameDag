using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
