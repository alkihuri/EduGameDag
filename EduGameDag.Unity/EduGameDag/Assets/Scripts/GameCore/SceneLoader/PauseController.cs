using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    float prevTimeScale;

    [SerializeField] GameObject _pauseMenu;
     
    const int PLAY = 1;
    const int PAUSE = 0; 
    public void Pause()
    {
        prevTimeScale = Time.timeScale;
        Time.timeScale = prevTimeScale == PLAY ? PAUSE : PLAY;
        _pauseMenu.SetActive(prevTimeScale != PAUSE); 

    }



}
