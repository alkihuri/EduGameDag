using System;
using System.Collections.Generic;
using DG.Tweening;
using GameCore;
using GameCore.Questions;
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

        private bool isQuestCounted;

        public static AjdahaDistanceController instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            
            QuestionGenerator.Instance.OnQuestCounted += InitialGame;
        }

        public void InitialGame()
        {
            previousDistance = QuestionGenerator.Instance.QuestionCount;
            ScoreController.instance.Score = 0;
            ChangeValueDistance();
            ScoreController.instance.OnScoreValueChange += ChangeValueDistance;
        }
        void ChangeValueDistance()
        {
            Debug.Log((QuestionGenerator.Instance.QuestionCount -ScoreController.instance.Score)
                      + "[" + Time.time.ToString("0.0") + "] ") ;
            if(QuestionGenerator.Instance.QuestionCount == 0)
                return;
            needDistance = (QuestionGenerator.Instance.QuestionCount - ScoreController.instance.Score) * 5-2;
            var increaseCount = 0f;
            if ((previousDistance-needDistance) > 0)
            {
                increaseCount = (100f / QuestionGenerator.Instance.QuestionCount / 100f);
            }
            else if ((previousDistance-needDistance) < 0)
            {
                //increaseCount = -(100f / QuestionGenerator.Instance.QuestionCount / 100f);
            }
            else
            {
                Debug.Log("increase = 0");
            }
            // сори за это явление говнокода :(
            distanceValueChanged?.Invoke(increaseCount);
            previousDistance = needDistance;
            transform.DOMove(new Vector3(listOfRoads[random].position.x, listOfRoads[random].position.y,
                playerTransform.position.z + needDistance), 1f);
        }
    }
}