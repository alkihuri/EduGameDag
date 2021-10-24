using GameCore.Questions;
using Inputs.Characters.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.QuestPrefabs
{
    public class AnswerObjectController : MonoBehaviour
    {
        [SerializeField]
        Color right, wrong;

        private float speed;

        private Transform player
        {
            get
            {
                return SaidController.instance.transform;
            }
        }

        public float Speed
        {
            get { return speed; }
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
            if (Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y,
                player.position.z)) < 10f)
            {
                speed = 0.5f;
            }
            else
            {
                speed = 1f;
            }

            transform.position -= new Vector3(0, 0, 0.1f) * speed;
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