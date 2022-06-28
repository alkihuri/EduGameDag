using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameCore.QuestPrefabs;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization; // просто что за неочевидное название баляд тупые животные
using Random = UnityEngine.Random;

namespace GameCore.Questions
{
    public class QuestionGenerator : MonoBehaviour
    {
        public static QuestionGenerator Instance;

        [SerializeField]
        private TextMeshProUGUI _text;

        public float slowSpeed;
        public float fastSpeed;

        [SerializeField]
        public List<Transform> listOfQuestionToSpawm;

        [SerializeField]
        private GameObject cube;

        private float _gameSpeed;

        private Quest[] _tempArrayOfQuests;

        public QuestLoader questLoader;

        [SerializeField]
        private List<string>
            listOfRightAnswers; // here we have to store короче все бкувы\ответы\слоги\слова которые нужно отгадать, с каждым успешным ответом будем миносовать

        public float GameSpeed
        {
            get => _gameSpeed;
            set => _gameSpeed = value;
        }

        public int QuestionCount { get; private set; }

        public event Action OnQuestCounted;

        private GameObject[] _objects = new GameObject[4];
        private int _currentQuest = -1;
        private int _questCountInPack;
        private int _currentQuestPack = -1;

        [FormerlySerializedAs("_num")] [SerializeField]
        private int num;

        public int CurrentQuestPack
        {
            get
            {
                return _currentQuestPack;
                _currentQuest = -1;
            }
            set => _currentQuestPack = value;
        }


        public event Action OnLoadNewSubject;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void CalculateQuestions()
        {
            QuestionCount = 0;
            foreach (Quest quest in questLoader.TourQuests.tourQuests[CurrentQuestPack].quests) QuestionCount += 1;

            OnQuestCounted?.Invoke();
        }

        private void ClearObjects()
        {
            if (listOfRightAnswers.Count < 1)
                GameStateController.instance.GameOver();

            if (_objects is not {Length: > 0}) return;
            foreach (GameObject obj in _objects)
            {
                if (obj == null) continue;
                Destroy(obj.GetComponent<BoxCollider>());
                Destroy(obj, 1);
            }
        }

        public void ObjectCleanup(GameObject ps) => Destroy(ps, 3f);

        private void Start()
        {
            OnLoadNewSubject += GenerateNewQuestLevel;
            GameStateController.instance.GameStarted += CalculateQuestions;
            OnQuestCounted += LoadNewSubject;
            OnQuestCounted += () => _tempArrayOfQuests = questLoader.QuestionPack.quests;
        }

        private void RightAnswersListInnit()
        {
            listOfRightAnswers = new List<string>();
            foreach (Quest line in questLoader.QuestionPack.quests) listOfRightAnswers.Add(line.right_answer);
        }

        public void RemoveKebab(string key) => listOfRightAnswers.Remove(key);

        private void LoadNewSubject()
        {
            _currentQuest = 0;
            InitializeQuest();
            RightAnswersListInnit();
            OnLoadNewSubject?.Invoke();
        }

        private void InitializeQuest()
        {
            try
            {
                questLoader.QuestionPack = questLoader.TourQuests.tourQuests[_currentQuestPack];
                _questCountInPack = questLoader.QuestionPack.questCount;
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log("Here we can catch game ending");
            }
        }

        private IEnumerator ChangeQuestion()
        {
            yield return new WaitForSeconds(4f);
            LoadNewSubject();
        }

        public void GenerateNewQuestLevel()
        {
            ClearObjects();
            if (listOfRightAnswers.Count > 0) GenerateCubes();
        }

        private bool CanGenerateDummyFunction(string key)
        {
            if (num == 2 && key.Contains("_"))
                return false;

            num = listOfRightAnswers.Count(word => word.Contains(key.Replace("_", "")) == true); // выпилить нахрен

            return listOfRightAnswers.Contains(key);
        }


        private void GenerateCubes()
        {
            try
            {
                var checkWord = questLoader.QuestionPack.quests[_currentQuest].right_answer;
                if (!CanGenerateDummyFunction(checkWord))
                {
                    _currentQuest++;
                    GenerateCubes();
                }
                else
                {
                    OldButGoldMethodTogenerate();
                }
            }
            catch
            {
                _currentQuest = 0;
                GenerateCubes();
            }
        }

        private void OldButGoldMethodTogenerate() /// как в солиде короче, нельзя меня старое говно
        {
            _text.text = questLoader.QuestionPack.quests[_currentQuest].question;
            for (var i = 0; i < 3; i++)
            {
                GameObject newOne = Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
                var answerText = questLoader.QuestionPack.quests[_currentQuest].wrong_answers[i].ToString();
                _objects[i] = newOne;
                newOne.GetComponent<AnswerObjectController>().SetWrong(answerText);
                newOne.GetComponent<AnswerObjectController>().SetSpeed(fastSpeed, fastSpeed);
            }

            GameObject rObj = Instantiate(cube, listOfQuestionToSpawm[3].position, Quaternion.identity);
            rObj.GetComponent<AnswerObjectController>().SetSpeed(fastSpeed, fastSpeed);
            _objects[3] = rObj;
            rObj.GetComponent<AnswerObjectController>()
                .SetRight(questLoader.QuestionPack.quests[_currentQuest].right_answer);
            ShuffleAnswers(ref _objects);
            _currentQuest++;
        }

        private static void ShuffleAnswers(ref GameObject[] array)
        {
            try
            {
                var lastShit = array.Length - 1;
                var randomIndex = Random.Range(0, 5);
                if (randomIndex == PlayerPrefs.GetInt("LastRightAns", randomIndex))
                    randomIndex = randomIndex >= 2 ? --randomIndex : ++randomIndex;

                (array[lastShit].transform.position, array[randomIndex].transform.position)
                    =
                    (array[randomIndex].transform.position, array[lastShit].transform.position);
                PlayerPrefs.SetInt("LastRightAns", randomIndex);
            }
            catch
            {
                Debug.LogWarning("Сокрытые. Буквы с краю короче");
            }
        }
    }
}
