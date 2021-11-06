using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameCore.QuestPrefabs;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameCore.Questions
{
    public class QuestionGenerator : MonoBehaviour
    {
        public static QuestionGenerator Instance;

        [SerializeField]
        private Text _text;

        [SerializeField]
        public  List<Transform> listOfQuestionToSpawm;

        [SerializeField]
        private GameObject cube;

        private float gameSpeed;

        Quest[] _tempArrayOfQuests; 

        public QuestLoader questLoader;
        [SerializeField]
        List<string> listOfRightAnswers;  // here we have to store короче все бкувы\ответы\слоги\слова которые нужно отгадать, с каждым успешным ответом будем миносовать

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

        public event Action OnQuestCounted;

        //private vars
        private GameObject[] objects = new GameObject[4];
        private int currentQuest = -1;
        private int questCountInPack;
        private int currentQuestPack = -1;

        public int CurrentQuestPack
        {
            get
            {
                return currentQuestPack;
                currentQuest = -1;
            }
            set
            {
                currentQuestPack = value;
                // StartCoroutine(ChangeQuestion());
            }
        }


        public event Action OnLoadNewSubject; //Событие, вызывающееся при появлении нового учебного предмета

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        //after game started
        public void CalculateQuestions()
        {
            questCount = 0;
            Debug.Log(CurrentQuestPack);
            foreach (var quest in questLoader.TourQuests.tourQuests[CurrentQuestPack].quests)
            {
                questCount += 1;
            }

            Debug.Log("quest calculatedd" + "[" + Time.time.ToString("0.0") + "] ");
            OnQuestCounted?.Invoke();
            //TODO: Remake THIS SHIT
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
            OnLoadNewSubject += GenerateNewQuestLevel; 
            GameStateController.instance.GameStarted += CalculateQuestions;
            OnQuestCounted += LoadNewSubject;
            _tempArrayOfQuests = questLoader.QuestionPack.quests;
            // questLoader.OnJsonLoaded += CalculateQuestions;
        }

        private void RightAnswersListInnit()
        {
            listOfRightAnswers = new List<string>();
            foreach (Quest line in questLoader.QuestionPack.quests)
            {
                listOfRightAnswers.Add(line.right_answer);
            }
        }
        public  void RemoveKebab(string key) // да я просто удаляю из листа
        {
            listOfRightAnswers.Remove(key);
        }

        // after questCounted
        private void LoadNewSubject()
        {
            currentQuest = -1;
            InitializeQuest(); 
            RightAnswersListInnit();
            OnLoadNewSubject?.Invoke();
        }

        private void InitializeQuest()
        {
            try
            {
                questLoader.QuestionPack = questLoader.TourQuests.tourQuests[currentQuestPack];
                questCountInPack = questLoader.QuestionPack.questCount;
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log("Here we can catch game ending");
                // TODO: Game Ended   
            }
        }

        private IEnumerator ChangeQuestion()
        {
            yield return new WaitForSeconds(4f);
            LoadNewSubject();
        }

        //after quest counted
        public void GenerateNewQuestLevel()
        {
            ClearObjects();
            if (listOfRightAnswers.Count>0)
            {
                currentQuest++;
                GenerateCubes();
                if (currentQuest > 4)
                    currentQuest = 0;
            }
            else
            {
                GameStateController.instance.GameOver();
            }
            // else
            // {
            //     Debug.Log("QuestionGenerator NEW SUBJECT LOADED");
            //     LoadNewSubject();
            // }
        }

        private void GenerateCubes() // кихури на исполнениях если покайу
        {

            try
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
                rObj.GetComponent<AnswerObjectController>()
                    .SetRight(questLoader.QuestionPack.quests[currentQuest].right_answer);
                ShuffleAnswers(ref objects);
            }
            catch
            {
                currentQuest = 0;
                GenerateCubes(); // smelly piece of shit
            }
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