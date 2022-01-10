using GameCore.Questions;
using Inputs.Characters.Scripts;
using UnityEngine; 
using TMPro;

namespace GameCore.QuestPrefabs
{
    public class AnswerObjectController : MonoBehaviour
    {
        public float _slowSpeed = 3.3f;
        public float _fastSpeed = 6.3f;
        private const int _distanceToSwitchSpeedMode = 10;
        [SerializeField]
        Material _rightMaterial, _wrongMaterial; 
        public GameObject _objectToColor;
        [SerializeField] GameObject _baloon;
        private float _speed;
        private string _label;
        const string OXI_TIP_CHARACTER = "_";
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
            get { return _speed; }
            set => _speed = value;
        }

        public void SetSpeed(float slow, float fast)
        {
            _slowSpeed = slow;
            _fastSpeed = fast;
        }

        [SerializeField]
        bool isRightAnswer;

        void Start()
        { 
            _speed = QuestionGenerator.Instance.GameSpeed;
        }

        void SetColor(Material  materialToSet)
        {
            _objectToColor.GetComponent<Renderer>().material = materialToSet;
        }

        public void SetRight(string label)
        {

            if (!label.Contains(OXI_TIP_CHARACTER))
                SetColor(_rightMaterial);
            else
                SetColor(_wrongMaterial);

            _label = label;
            label = label.Replace(OXI_TIP_CHARACTER, ""); /// это надо будет выпилить. даже не вникай
            _textLabel.text = label;
            isRightAnswer = true;
            PlayerPrefs.SetString("CURRENT_AUDIO_KEY", label);
        }

        public void SetWrong(string label)
        {
            _label = label;
            _textLabel.text = label;
            isRightAnswer = false;
            SetColor(_wrongMaterial);
        }

        private void FixedUpdate()
        {
            var distance = Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y, player.position.z));
            _speed = distance < _distanceToSwitchSpeedMode ? _slowSpeed : _fastSpeed; // гьай гьай баляд рефакторинг 
            transform.position -= new Vector3(0, 0, 0.1f) * _speed;
        }

        public void RealeseBaloon()
        {
            _speed = _fastSpeed;
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
           // else
                //ScoreController.instance.Score--;
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