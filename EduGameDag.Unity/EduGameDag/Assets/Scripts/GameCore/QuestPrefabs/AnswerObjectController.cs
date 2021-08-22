﻿using GameCore.Qustions;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.QuestPrefabs
{
    public class AnswerObjectController : MonoBehaviour
    {
        [SerializeField]
        Color right, wrong;

        [SerializeField]
        bool isRightAnswer;

        void Start()
        {
            right = Color.green;
            wrong = Color.red;
        }

        void SetColor(Color colorToSet)
        {
            GetComponentInChildren<Renderer>().material.color = colorToSet;
        }

        public void SetRight(string label)
        {
            GetComponentInChildren<Text>().text = label;
            isRightAnswer = true;
            SetColor(right);
        }

        public void SetWrong(string label)
        {
            GetComponentInChildren<Text>().text = label;
            isRightAnswer = false;
            SetColor(wrong);
        }

        private void Update()
        {
            transform.position -= new Vector3(0, 0, 0.1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<SaidController>()) return;
            if (isRightAnswer)
                QustionsAnswers.scores++;
            else
                QustionsAnswers.scores--;
            QuestionGenerator.Instance.GenerateNewLevel();
            Destroy(this.gameObject);
        }
    }
}