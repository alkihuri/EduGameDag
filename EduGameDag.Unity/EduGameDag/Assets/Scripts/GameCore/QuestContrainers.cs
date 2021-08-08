using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int questCount; //TODO: Create smth for this
}