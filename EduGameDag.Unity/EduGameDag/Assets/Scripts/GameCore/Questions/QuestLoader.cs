using System;
using System.Collections;
using GameCore.Qustions;
using UnityEngine;

namespace GameCore
{
    public class QuestLoader : MonoBehaviour
    {
        private QuestPack questionPack;
        private Tour tourQuest;

        public QuestPack QuestionPack
        {
            get
            {
                return questionPack;
            }
            set
            {
                questionPack = value;
            }
        }

        public Tour TourQuest
        {
            get
            {
                return tourQuest;
            }
        }
        public TextAsset jsonFile;
        public QuestionGenerator QuestionGenerator;
        public event Action OnJsonLoaded;
        
        private void Start()
        {
            tourQuest = JsonUtility.FromJson<Tour>(jsonFile.text);
            StartCoroutine(CheckLoadedJson());

        }
        
        private IEnumerator CheckLoadedJson()
        {
            while (tourQuest == null)
                yield return null;
            Debug.Log("tour not null");
            OnJsonLoaded?.Invoke();
            yield return null;
        }
        
    }
}