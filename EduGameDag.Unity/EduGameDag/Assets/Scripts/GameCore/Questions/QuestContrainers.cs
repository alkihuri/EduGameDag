using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public class Quest
    {
        public string question;
        public string[] wrong_answers;
        public string right_answer;
    }
    [System.Serializable]
    public class QuestPack
    {
        public Quest[] quests;
        public string tourName;
        public int questCount; //TODO: Create smth for this
    }
    [System.Serializable]
    public class Tour
    {
        public QuestPack[] tourQuests;
    }
}