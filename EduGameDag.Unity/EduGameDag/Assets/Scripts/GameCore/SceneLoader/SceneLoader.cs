using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance; // тоже поисполняю в  публик синглтон
        public const string WIN_SCENE_NAME = "Win";
        public const string LOSE_SCENE_NAME = "Lose";
        public const string GAME_SCENE_NAME = "DemoScene";
        public const string BOOT_SCENE_NAME = "Boot";
        public const string LEVEL_SELECT_SCENE_NAME = "____"; // todo
        public const string CURRENT_STATE_SCENE_KEY = "CurrentSceneState";
        public List<Scene> sceneList;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            sceneList = new List<Scene>();
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, GAME_SCENE_NAME);
            //А эта штука вызывает рекурсию и бесконечное кол-во сцен
            CheckScene();
        }

        private void Update()
        {
            CheckScene();
        }

        private void CheckScene()
        {
            sceneList = SceneManager.GetAllScenes().ToList();
            var needScene = PlayerPrefs.GetString(CURRENT_STATE_SCENE_KEY);
            if (sceneList.Where(scene => scene.name == needScene).Count() == 0)
            {
                SetCurrentSceneState(needScene);
                SceneManager.LoadScene(needScene, LoadSceneMode.Additive);
            }
            else
            {
                foreach( Scene  scene in sceneList )
                {
                    if( scene.name != needScene)
                    {
                        CloseScene(scene.name);
                    }
                }
            }
        }

        public void CloseScene(string scene)
        {
            if (scene != BOOT_SCENE_NAME)
            {
                SceneManager.UnloadScene(scene);
            }
        }

        public void SetCurrentSceneState(string rule) // lose -> select -> game
        {
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, rule);
           
        }

        public void SetWinScene()
        {
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, WIN_SCENE_NAME);
            StartCoroutine(DummyTimer(10));
        }

        public void SetLoseScene()
        {
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, LOSE_SCENE_NAME);
            StartCoroutine(DummyTimer(10));
        }

        private IEnumerator DummyTimer(float sec)
        {
            yield return new WaitForSeconds(sec);
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, GAME_SCENE_NAME);
        }

    }
}