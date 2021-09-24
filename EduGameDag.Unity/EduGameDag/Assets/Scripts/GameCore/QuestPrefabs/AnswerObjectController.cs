using GameCore.Qustions;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.QuestPrefabs
{
    public class AnswerObjectController : MonoBehaviour
    {
        [SerializeField]
        Color right, wrong;

        private float speed;

        public float Speed
        {
            get
            {
                return speed;
            }
            set => speed = value;
        }
        [SerializeField]
        bool isRightAnswer;

        void Start()
        {
            right = Color.green;
            wrong = Color.red;
            speed = QuestionGenerator.Instance.GameSpeed;
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
                ScoreController.instance.Score++;
            else
                ScoreController.instance.Score--;
            QuestionGenerator.Instance.GenerateNewQuestLevel();
            Destroy(this.gameObject);
        }
    }
}