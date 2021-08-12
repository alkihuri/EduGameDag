using System;
using System.Collections;
using System.Collections.Generic;
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
        }

        private void Start()
        {
            tourQuest = JsonUtility.FromJson<Tour>(jsonFile.text);
            StartCoroutine(ChangeQuestion());
            OnLoadNewSubject += GenerateNewLevel;
        }

        public void LoadNewSubject()
        {
            currentQuestPack += 1;
            currentQuest = 0;
            InitializeQuest();
            OnLoadNewSubject?.Invoke();
        }

        private void InitializeQuest()
        {
            questPack = tourQuest.tourQuests[currentQuestPack];
            questCountInPack = questPack.questCount;
            Debug.Log("Quests initialized");
        }

        private IEnumerator ChangeQuestion()
        {
            yield return new WaitForSeconds(4f);
            LoadNewSubject();
        }
    
        public void GenerateNewLevel()
        {
            if (currentQuest != questCountInPack)
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
            Debug.Log(questPack.quests.Length);
            _text.text = questPack.quests[currentQuest-1].question;
            for (var i = 0; i < 3; i++)
            {
                var newOne = Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
                var answerText = questPack.quests[currentQuest-1].wrong_answers[i].ToString();
                objects[i] = newOne;
                newOne.GetComponent<AnswerObjectController>().SetWrong(answerText);
            }

            var rObj = Instantiate(cube, listOfQuestionToSpawm[3].position, Quaternion.identity);
            objects[3] = rObj;
            rObj.GetComponent<AnswerObjectController>().SetRight(questPack.quests[currentQuest-1].right_answer);
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