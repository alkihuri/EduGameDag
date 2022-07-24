using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMusicController : MonoBehaviour
{
    [SerializeField] MusicControler _music;
    [SerializeField] bool Invert;

    private void Awake()
    {
        _music = GameObject.FindObjectOfType<MusicControler>();
    }

    private void OnEnable()
    {
        if(Invert)
            _music.FadeMusic();
        else 
            _music.RiseMusic();
    }

    private void OnDisable()
    {
        if (Invert) 
            _music.RiseMusic();
        else
            _music.FadeMusic();
    }
}
