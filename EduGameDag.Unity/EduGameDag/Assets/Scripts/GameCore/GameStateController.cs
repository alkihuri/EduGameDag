using System;
using System.Collections;
using GameCore.Questions;
using UnityEngine;

namespace GameCore
{
    public class GameStateController : MonoBehaviour
    {
        public static GameStateController instance;
        public event Action GameEndEvent;

        public event Action GameStarted;
        [SerializeField] QuestionGenerator _questionGenerator;
        [SerializeField] int _difference;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            _questionGenerator = GameObject.FindObjectOfType<QuestionGenerator>();
            GameEndEvent += CountScores;
        }

        public void GameOver()
        {
            GameEndEvent?.Invoke();
        }

        public void StartGame()
        {
            GameStarted?.Invoke();
        }

        private void CountScores()
        {
            StartCoroutine(DelayWinScene());
        }

        IEnumerator DelayWinScene()
        { 
            yield return new WaitForSeconds(1);
            SceneLoader.SceneLoader.instance.SetWinScene();
        }
    }
}