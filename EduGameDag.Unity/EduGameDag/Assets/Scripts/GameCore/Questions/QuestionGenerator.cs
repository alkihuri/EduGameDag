using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.QuestPrefabs;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameCore.Qustions
{
    public class QuestionGenerator : MonoBehaviour
    {
        public static QuestionGenerator Instance;

        [SerializeField]
        private Text _text;

        [SerializeField]
        private List<Transform> listOfQuestionToSpawm;

        [SerializeField]
        private GameObject cube;

        private float gameSpeed;

        public QuestLoader questLoader;

        public float GameSpeed
        {
            get { return gameSpeed; }
            set => gameSpeed = value;
        }

        private int questCount;

        public int QuestionCount
        {
            get { return questCount; }
        }


        //private float
        private GameObject[] objects = new GameObject[4];
        private int currentQuest = -1;
        private int questCountInPack;
        private int currentQuestPack = -1;
        public event Action OnLoadNewSubject; //Событие, вызывающееся при появлении нового предмета

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            questLoader.OnJsonLoaded += CalculateQuestions;
        }

        private void CalculateQuestions()
        {
            questCount = 0;
            foreach (var qPack in questLoader.TourQuest.tourQuests)
            {
                questCount += qPack.questCount;
            }
        }

        private void ClearObjects()
        {
            if (objects != null && objects.Length > 0)
            {
                foreach (var obj in objects)
                {
                    Destroy(obj);
                }
            }
        }

        private void Start()
        {
            StartCoroutine(ChangeQuestion());
            OnLoadNewSubject += GenerateNewLevel;
        }


        private void LoadNewSubject()
        {
            currentQuestPack += 1;
            currentQuest = -1;
            InitializeQuest();
            OnLoadNewSubject?.Invoke();
        }

        private void InitializeQuest()
        {
            try
            {
                questLoader.QuestionPack = questLoader.TourQuest.tourQuests[currentQuestPack];
                questCountInPack = questLoader.QuestionPack.questCount;
            }
            catch (IndexOutOfRangeException e)
            {
                // TODO: Game Ended   
            }
        }

        private IEnumerator ChangeQuestion()
        {
            yield return new WaitForSeconds(4f);
            LoadNewSubject();
        }

        public void GenerateNewLevel()
        {
            ClearObjects();
            if (currentQuest < questCountInPack - 1)
            {
                currentQuest++;
                GenerateCubes();
            }
            else
            {
                LoadNewSubject();
            }
        }

        private void GenerateCubes()
        {
            _text.text = questLoader.QuestionPack.quests[currentQuest].question;
            for (var i = 0; i < 3; i++)
            {
                var newOne = Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
                var answerText = questLoader.QuestionPack.quests[currentQuest].wrong_answers[i].ToString();
                objects[i] = newOne;
                newOne.GetComponent<AnswerObjectController>().SetWrong(answerText);
            }

            var rObj = Instantiate(cube, listOfQuestionToSpawm[3].position, Quaternion.identity);
            objects[3] = rObj;
            rObj.GetComponent<AnswerObjectController>().SetRight(questLoader.QuestionPack.quests[currentQuest].right_answer);
            ShuffleAnswers(ref objects);
        }

        private static void ShuffleAnswers(ref GameObject[] array)
        {
            var n = array.Length;
            while (n > 1)
            {
                n -= 1;
                var k = Random.Range(0, n);
                (array[n].transform.position, array[k].transform.position) = (array[k].transform.position,
                    array[n].transform.position);
            }
        }
    }
}