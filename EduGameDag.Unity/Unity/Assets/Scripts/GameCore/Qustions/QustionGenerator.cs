using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QustionGenerator : MonoBehaviour
{
    [SerializeField] Text _text;
    public int currentQuest = -1;
    [SerializeField] List<Transform> listOfQuestionToSpawm;
    [SerializeField] GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
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

        _text.text = QustionsAnswers.questions.Split('#')[currentQuest];
        for(int i =0;i<4;i++)
        {
           GameObject newOne =  Instantiate(cube, listOfQuestionToSpawm[i].position, Quaternion.identity);
            string text = QustionsAnswers.answers.Split('#')[currentQuest].Split(',')[i];
            if (text.Contains("_"))
            { 
                newOne.GetComponent<QustController>().SetRight(text);
            }
            else
            {
                newOne.GetComponent<QustController>().SetWrong(text);
            }
        }

    }
}
