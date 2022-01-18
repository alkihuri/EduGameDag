using System;
using UnityEngine;

namespace GameCore
{
    public class ScoreController : MonoBehaviour
    {
        public static ScoreController instance;

        public event Action OnScoreValueChange;
        public event Action<int> OnScoreValueChangedWithParameter;
        private float score;
        private int previousScore;

        public float Score
        {
            get => score;
            set
            {
                score = value;
                previousScore = (int) (score - previousScore);
                OnScoreValueChange?.Invoke();
            }
        }
        private void Awake()
        {
            if (instance == null)
                instance = this;
            OnScoreValueChange += delegate
            {
                OnScoreValueChangedWithParameter?.Invoke(previousScore);
            };
        }
    }
}