using GameCore.Questions;
using Inputs.Characters.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace GameCore.QuestPrefabs
{
    public class AnswerObjectController : MonoBehaviour
    {
        [SerializeField]
        Color right, wrong;

        private float speed;
        private string _label;
        //тупо собаки придумали какуюто дичь с текстмешпрогуй, а есть просто текстмешпро и хрен разбери сигаман
        [SerializeField] TextMeshProUGUI _textLabel; 
        // ана бозарган азиз ящлаган
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
            _label = label;
            _textLabel.text = label;
            isRightAnswer = true;
            SetColor(right);
            PlayerPrefs.SetString("CURRENT_AUDIO_KEY", label);
        }

        public void SetWrong(string label)
        {
            _label = label;
            _textLabel.text = label;
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
            {
                RemoveKebabInvoker(_label);
                ScoreController.instance.Score++;
            }
            else
                ScoreController.instance.Score--;
            QuestionGenerator.Instance.GenerateNewQuestLevel();
            Destroy(this.gameObject);
        }
        // hardcode
        [SerializeField] QuestionGenerator _questionGenerator;
        public void RemoveKebabInvoker(string key)
        {
            _questionGenerator = GameObject.FindObjectOfType<QuestionGenerator>();
            _questionGenerator.RemoveKebab(key);
        }
        
    }
}