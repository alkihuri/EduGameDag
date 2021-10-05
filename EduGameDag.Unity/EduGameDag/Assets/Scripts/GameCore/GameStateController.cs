using System;
using GameCore.Qustions;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.SceneManagement;

namespace GameCore
{
    public class GameStateController : MonoBehaviour
    {
        public static GameStateController instance;
        public event Action GameEndEvent;

        public event Action GameStarted;

        private void Awake()
        {
            if(instance == null)
                instance = this;
        }
        private void Start()
        {
            GameEndEvent += CountScores;
        }


        private void Update()
        {
            // CountScores(); наебал да я что дурак чтоли
        }

        public void StartGame()
        {
            GameStarted?.Invoke();
        } 
        private void CountScores()
        {
 
            var difference = QuestionGenerator.Instance.QuestionCount - ScoreController.instance.Score;
            if (difference == 0)
            {
                Debug.Log("win ebat");
                new SceneLoader().instance.SetWinScene();
                //TODO: EZ WIN 
            }
            else if (difference<5 && difference>0)
            {
                Debug.Log("win ebat");
                new SceneLoader().instance.SetWinScene();
                //TODO: not ez win
            }
            else if (difference > 5)
            {
                Debug.Log("lose ebat");
                new SceneLoader().instance.SetLoseScene();
                //TODO: LOSE
            }
        }
    }
}


 
