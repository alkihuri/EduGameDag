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

        private float needDistance;

        private void Start()
        {
            ScoreController.instance.OnScoreValueChange += ChangeValueDistance;
            QuestionGenerator.Instance.JsonLoaded += () => { ScoreController.instance.Score = 0; };
        }

        void ChangeValueDistance()
        {
            needDistance = (QuestionGenerator.Instance.QuestionCount - ScoreController.instance.Score);
            distanceValueChanged?.Invoke(0.16f);
            Debug.Log(QuestionGenerator.Instance.QuestionCount);
            transform.DOMove(new Vector3(listOfRoads[random].position.x, listOfRoads[random].position.y,
                playerTransform.position.z + needDistance), 1f);
        }
    }
}