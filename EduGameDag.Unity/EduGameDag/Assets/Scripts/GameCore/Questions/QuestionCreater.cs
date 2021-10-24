using GameCore.Questions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCreater : MonoBehaviour
{
    [SerializeField] Text tx;
    public TextAsset jsonFile;
    public void DummyTester(string data)
    {
        InputData(data);
    }

    public void InputData(string data, string type = "default")
    {
        
        switch (type)
        {
            case "Google Table":
                DoGoogleTable(data);
            break;

            default:
                DoDefault(data);
            break;
        }
    }
    void DoGoogleTable(string data)
    {
       // no
    }
    void DoDefault(string data)
    {
        Tour tour = new Tour(); 
        QuestPack qp = new QuestPack();
        Quest quest = new Quest();
        quest.question = "Как дела?";
        quest.right_answer = 1.ToString();
        quest.wrong_answers = new string [3] { 2.ToString(), 3.ToString(), 4.ToString() };
        qp.tourName = "Предмет #" + Random.Range(1111, 9999); // data.Split(';')[0];
        qp.quests = new Quest[1] { quest };
        List<QuestPack> questPackList = new List<QuestPack>();
        questPackList.Add(qp);
        tour.tourQuests = questPackList.ToArray();
         
         
    }

    public void PutTourOnDb(Tour t, string path = "")
    {
        PlayerPrefs.SetString("TEMP_ADDITIONAL_QS", JsonUtility.ToJson(t));
    }

}
