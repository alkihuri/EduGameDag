using System.Collections.Generic;
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
            // SetCurrentSceneState(GAME_SCENE_NAME); // начнем
        }

        void Update()
        {
            //Уцы, эта суета нонстопом сцены спамила )))
            // var needScene = PlayerPrefs.GetString(CURRENT_STATE_SCENE_KEY);
            // sceneList = SceneManager.GetAllScenes().ToList();
            // if (sceneList.Where(scene => scene.name != needScene).Count() == 0)
            //     SetCurrentSceneState(needScene);
        }

        public void CloseScene(string scene)
        {
            if (scene != BOOT_SCENE_NAME)
            {
                SceneManager.UnloadScene(scene);
            }
            else
                Debug.Log("клубника бомба");
        }

        public void SetCurrentSceneState(string rule) // lose -> select -> game
        {
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, rule);
            SceneManager.LoadSceneAsync(rule, LoadSceneMode.Additive);
        }

        public void SetWinScene()
        {
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, WIN_SCENE_NAME);
        }

        public void SetLoseScene()
        {
            PlayerPrefs.SetString(CURRENT_STATE_SCENE_KEY, LOSE_SCENE_NAME);
        }
    }
}