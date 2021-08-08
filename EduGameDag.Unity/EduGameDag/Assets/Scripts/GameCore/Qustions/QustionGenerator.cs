using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QustionGenerator : MonoBehaviour
{
    [SerializeField] Text _text;
    public TextAsset jsonFile;
    private QuestPack questPack;
    
    public int currentQuest = -1;
    [SerializeField] List<Transform> listOfQuestionToSpawm;
    [SerializeField] GameObject cube;
    GameObject[] objects = new GameObject[4];

    void Start()
    {
        questPack = JsonUtility.FromJson<QuestPack>(jsonFile.text);
        StartCoroutine(CahngeQuestion());
    }
    IEnumerator CahngeQuestion()
    {
        while(QustionsAnswers.gameIsOn == true)
        {
            yield return new WaitForSeconds(8);
            currentQuest++;
            VoidGenerateCubes();
        }
    }

    public void VoidGenerateCubes()
    {
        _text.text = questPack.quests[currentQuest].question;
        for (int i = 0; i<3; i++)
        {
            GameObject newOne = Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
            string text = questPack.quests[currentQuest].wrong_answers[i].ToString();
            objects[i] = newOne;
            newOne.GetComponent<QustController>().SetWrong(text);
        }
        var rObj = Instantiate(cube, listOfQuestionToSpawm[3].position, Quaternion.identity);
        objects[3] = rObj;
        rObj.GetComponent<QustController>().SetRight("FSDF");
        rObj.GetComponent<QustController>().SetRight(questPack.quests[currentQuest].right_answer);
        Shuffle(ref objects);

        //_text.text = QustionsAnswers.questions.Split('#')[currentQuest];
        //for(int i =0;i<4;i++)
        //{
        //    GameObject newOne =  Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
        //    string text = QustionsAnswers.answers.Split('#')[currentQuest].Split(',')[i];
        //    if (text.Contains("_"))
        //    { 
        //        newOne.GetComponent<QustController>().SetRight(text);
        //    }
        //    else
        //    {
        //        newOne.GetComponent<QustController>().SetWrong(text);
        //    }
        //}
  
    }
    public void Shuffle(ref GameObject[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n -= 1;
            int k = Random.Range(0, n);
            Vector3 tempPosition = array[n].transform.position;
            array[n].transform.position = array[k].transform.position;
            array[k].transform.position = tempPosition;
        }
    }
}
