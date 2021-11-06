using System;
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
            Debug.Log("Counted");

            var difference = _questionGenerator.listOfQuestionToSpawm.Count;//QuestionGenerator.Instance.QuestionCount - ScoreController.instance.Score;
            if (difference == 0)
            {
                Debug.Log("win ");
                SceneLoader.SceneLoader.instance.SetWinScene();
                //TODO: EZ WIN 
            }
            else if (difference < 5 && difference > 0)
            {
                Debug.Log("win not easy");
                SceneLoader.SceneLoader.instance.SetWinScene();
                //TODO: not ez win
            }
            else if (difference > 5)
            {
                Debug.Log("lose");
                SceneLoader.SceneLoader.instance.SetLoseScene();
                //TODO: LOSE
            }
             
        }
    }
}