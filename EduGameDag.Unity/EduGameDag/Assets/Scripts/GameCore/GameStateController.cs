using System;
using GameCore.Qustions;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace GameCore
{
    public class GameStateController : MonoBehaviour
    {
        public event Action GameEndEvent;

        private void Start()
        {
            GameEndEvent += CountScores;
        }

        private void CountScores()
        {
            var difference = QuestionGenerator.Instance.QuestionCount - ScoreController.instance.Score;
            if (difference == 0)
            {
                //TODO: EZ WIN
            }
            else if (difference<5 && difference>0)
            {
                //TODO: not ez win
            }
            else if (difference > 5)
            {
                //TODO: LOSE
            }
        }
    }
}