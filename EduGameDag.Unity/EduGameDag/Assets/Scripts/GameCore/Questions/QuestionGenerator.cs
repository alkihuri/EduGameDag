using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameCore.QuestPrefabs;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // просто что за неочевидное название баляд тупые животные
using Random = UnityEngine.Random;

namespace GameCore.Questions
{
    public class QuestionGenerator : MonoBehaviour
    {
        public static QuestionGenerator Instance;

        [SerializeField]
        private TextMeshProUGUI _text;

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
        [SerializeField] private int _num;

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

            if (listOfRightAnswers.Count < 1)
                GameStateController.instance.GameOver();

            if (objects != null && objects.Length > 0)
            {
                foreach (var obj in objects)
                {
                    Destroy(obj, 1);
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
            currentQuest = 0;
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
                GenerateCubes();
            }
           
            // else
            // {
            //     Debug.Log("QuestionGenerator NEW SUBJECT LOADED");
            //     LoadNewSubject();
            // }
        }

        bool CanGenerateDummyFunction(string key)
        {

            if (_num == 2 && key.Contains("_"))
                return false;

            _num = listOfRightAnswers.Where(word => word.Contains(key.Replace("_", "")) == true).Count(); // выпилить нахрен

           

            if (listOfRightAnswers.Contains(key))
                return true;
            else
                return false;
        }


        private void GenerateCubes() // кихури на исполнениях если покайу
        { 
            
            try
            { 
                var checkWord = questLoader.QuestionPack.quests[currentQuest].right_answer;
                if (!CanGenerateDummyFunction(checkWord))
                {
                   // Debug.Log(checkWord + " уже изучен");
                    currentQuest++;
                    GenerateCubes();
                }
                else
                {
                    OldButGoldMethodTogenerate();
                }
            }
            catch
            {
                currentQuest = 0;  
                GenerateCubes();
            }
        }

        private void OldButGoldMethodTogenerate() /// как в солиде короче, нельзя меня старое говно
        {
            _text.text = questLoader.QuestionPack.quests[currentQuest].question;
            for (var i = 0; i < 3; i++)
            {
                var newOne = Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
                var answerText = questLoader.QuestionPack.quests[currentQuest].wrong_answers[i].ToString();
                objects[i] = newOne;
                newOne.GetComponent<AnswerObjectController>().SetWrong(answerText);
            }

            GameObject rObj = Instantiate(cube, listOfQuestionToSpawm[3].position, Quaternion.identity);
            objects[3] = rObj;
            rObj.GetComponent<AnswerObjectController>().SetRight(questLoader.QuestionPack.quests[currentQuest].right_answer);
            ShuffleAnswers(ref objects);
            currentQuest++;
        }

        private static void ShuffleAnswers(ref GameObject[] array)
        {
            try
            { 
                int lastShit = array.Length - 1;
                int randomIndex = Random.Range(0, 3); 
                if(randomIndex == PlayerPrefs.GetInt("LastRightAns", randomIndex))
                {
                     randomIndex = randomIndex >= 2 ? --randomIndex : ++randomIndex;
                } 
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