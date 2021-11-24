using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
     public void LoadSceneMethod(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
            SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
