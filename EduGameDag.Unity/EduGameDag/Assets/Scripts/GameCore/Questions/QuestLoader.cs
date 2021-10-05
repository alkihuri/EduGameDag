using System;
using System.Collections;
using GameCore.Questions;
using UnityEngine;

namespace GameCore
{
    public class QuestLoader : MonoBehaviour
    {
        private QuestPack questionPack;
        private Tour _tourQuests;

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

        public Tour TourQuests
        {
            get
            {
                return _tourQuests;
            }
        }
        public TextAsset jsonFile;
        public QuestionGenerator QuestionGenerator;
        public event Action OnJsonLoaded;
        
        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            _tourQuests = JsonUtility.FromJson<Tour>(jsonFile.text);
            StartCoroutine(CheckLoadedJson());
        }
        
        private IEnumerator CheckLoadedJson()
        {
            while (_tourQuests == null)
                yield return null;
            Debug.Log("tour not null");
            yield return new WaitForSeconds(1f);
            OnJsonLoaded?.Invoke();
            yield return null;
        }
        
    }
}