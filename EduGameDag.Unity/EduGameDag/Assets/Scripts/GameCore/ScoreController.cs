using System;
using UnityEngine;

namespace GameCore
{
    public class ScoreController : MonoBehaviour
    {
        public static ScoreController instance;

        public event Action OnScoreValueChange;
        private float score;

        public float Score
        {
            get => score;
            set
            {
                score = value;
                OnScoreValueChange?.Invoke();
            }
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
    }
}