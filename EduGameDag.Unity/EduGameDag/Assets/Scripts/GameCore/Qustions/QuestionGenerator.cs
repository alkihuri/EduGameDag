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
        public TextAsset jsonFile;

        [SerializeField]
        private Text _text;

        [SerializeField]
        private List<Transform> listOfQuestionToSpawm;

        [SerializeField]
        private GameObject cube;

        public event Action OnJsonLoaded;
        private float gameSpeed;

        public float GameSpeed
        {
            get
            {
                return gameSpeed;
            }
            set => gameSpeed = value;
        }

        private int questCount;
        public int QuestionCount
        {
            get
            {

                return questCount;
            }
        }


        //private float
        private GameObject[] objects = new GameObject[4];
        private QuestPack questPack;
        private Tour tourQuest;
        private int currentQuest = -1;
        private int questCountInPack;
        private int currentQuestPack = -1;
        public event Action OnLoadNewSubject; //Событие, вызывающееся при появлении нового предмета

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            OnJsonLoaded += CalculateQuestions;
        }
        
        private void CalculateQuestions()
        {
            questCount = 0;
            foreach (var qPack in tourQuest.tourQuests)
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
            tourQuest = JsonUtility.FromJson<Tour>(jsonFile.text);
            StartCoroutine(CheckLoadedJson());
            StartCoroutine(ChangeQuestion());
            OnLoadNewSubject += GenerateNewLevel;
        }

        private IEnumerator CheckLoadedJson()
        {
            while (tourQuest == null)
                yield return null;
            Debug.Log("tour not null");
            OnJsonLoaded?.Invoke();
            yield return null;
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
                questPack = tourQuest.tourQuests[currentQuestPack];
                questCountInPack = questPack.questCount;    
            }catch(IndexOutOfRangeException e)
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
            if (currentQuest < questCountInPack-1)
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
            _text.text = questPack.quests[currentQuest].question;
            for (var i = 0; i < 3; i++)
            {
                var newOne = Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
                var answerText = questPack.quests[currentQuest].wrong_answers[i].ToString();
                objects[i] = newOne;
                newOne.GetComponent<AnswerObjectController>().SetWrong(answerText);
            }
            var rObj = Instantiate(cube, listOfQuestionToSpawm[3].position, Quaternion.identity);
            objects[3] = rObj;
            rObj.GetComponent<AnswerObjectController>().SetRight(questPack.quests[currentQuest].right_answer);
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