using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class SceneController : MonoBehaviour
{
   
     public void LoadSceneMethod(string sceneName)
    {
        var currentScene = SceneManager.GetActiveScene().name;
        if ((currentScene != sceneName) || (currentScene == "DemoScene" ))
            SceneManager.LoadScene(sceneName);
         
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
