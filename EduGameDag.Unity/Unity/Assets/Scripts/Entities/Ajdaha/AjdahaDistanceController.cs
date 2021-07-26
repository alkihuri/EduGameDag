using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjdahaDistanceController : MonoBehaviour
{

    [SerializeField, Range(1, 25)] public  float distance = 15;
    [SerializeField] Transform playerTransform;
    [SerializeField] List<Transform> listOfRoads;
    [SerializeField,Range(0,4)] int random = 1;
    float randomSinShit = 0;

    void Start()
    {
        
    }
    float needDisctande;
    void Update()
    {

          needDisctande = (QustionsAnswers.maxNumOfQust -  QustionsAnswers.scores) * 5;

        if (needDisctande > distance)
            distance += Time.deltaTime * 3f ;
        else
            distance -= Time.deltaTime * 3f;

        transform.position = new Vector3(listOfRoads[random].position.x, listOfRoads[random].position.y  , playerTransform.position.z + distance);
    }
}
