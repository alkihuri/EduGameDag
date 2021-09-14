using System;
using System.Collections.Generic;
using DG.Tweening;
using GameCore;
using GameCore.Qustions;
using UnityEngine;

namespace Entities.Ajdaha
{
    public class AjdahaDistanceController : MonoBehaviour
    {
        [SerializeField, Range(1, 25)]
        public float distance = 15;

        [SerializeField]
        Transform playerTransform;

        [SerializeField]
        List<Transform> listOfRoads;

        [SerializeField, Range(0, 4)]
        int random = 1;
        
        public event Action<float> distanceValueChanged;

        public float NeedDistance
        {
            get { return needDistance; }
            private set
            {
                needDistance = value;
                distanceValueChanged?.Invoke((100 / needDistance) / 100);
            }
        }

        public float PreviousDistance
        {
            get { return needDistance; }
            set
            {
                needDistance = value;
            }
        }
        private float previousDistance;
        private float needDistance;

        private void Start()
        {
            QuestionGenerator.Instance.questLoader.OnJsonLoaded += () =>
            {
                previousDistance = QuestionGenerator.Instance.QuestionCount;
                ScoreController.instance.Score = 0;
                ChangeValueDistance();
                ScoreController.instance.OnScoreValueChange += ChangeValueDistance;
            };
        }
        void ChangeValueDistance()
        {
            needDistance = (QuestionGenerator.Instance.QuestionCount - ScoreController.instance.Score);
            var increaseCount = (previousDistance - needDistance) > 0
                ? (100f / QuestionGenerator.Instance.QuestionCount / 100f)
                : -(100f / QuestionGenerator.Instance.QuestionCount / 100f); // сори за это явление говнокода :(
            Debug.Log(QuestionGenerator.Instance.QuestionCount);
            Debug.Log(increaseCount + " increase Count");
            distanceValueChanged?.Invoke(increaseCount);
            previousDistance = needDistance;
            transform.DOMove(new Vector3(listOfRoads[random].position.x, listOfRoads[random].position.y,
                playerTransform.position.z + needDistance), 1f);
        }
    }
}