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
        private const float _slowSpeed = 0.2f;
        private const float _fastSpeed = 5f;
        [SerializeField]
        Material right, wrong;
        [SerializeField] GameObject _baloon;
        private float speed;
        private string _label;
        //тупо собаки придумали какуюто дичь с текстмешпрогуй, а есть просто текстмешпро и хрен разбери сигаман
        [SerializeField] TextMeshProUGUI _textLabel;

        public GameObject _objectToColor;
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
            speed = QuestionGenerator.Instance.GameSpeed;
        }

        void SetColor(Material  materialToSet)
        {
            _objectToColor.GetComponent<Renderer>().material = materialToSet;
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
            var distance = Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y, player.position.z));
            speed = distance < 10 ? _slowSpeed : _fastSpeed; // гьай гьай баляд рефакторинг 
            transform.position -= new Vector3(0, 0, 0.1f) * speed;
        }

        public void RealeseBaloon()
        {
            _baloon.transform.SetParent(null);
            _baloon.GetComponent<BaloonController>().BreakSpring(); 
        }

        private void OnTriggerEnter(Collider other)
        {
           
            if (!other.gameObject.GetComponent<SaidController>()) 
                return;
            if (isRightAnswer)
            {
             
                RemoveKebabInvoker(_label);
                ScoreController.instance.Score++;
            }
            else
                ScoreController.instance.Score--;
            QuestionGenerator.Instance.GenerateNewQuestLevel();
            RealeseBaloon();
            Destroy(gameObject);
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