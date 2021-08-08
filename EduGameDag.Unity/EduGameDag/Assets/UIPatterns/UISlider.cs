using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField, Range(0, 100)] float distance;
     void Update()
    {
        distance = GameObject.FindObjectOfType<AjdahaDistanceController>().distance / QustionsAnswers.maxNumOfQust * 5;
        GetComponent<Slider>().value = distance /100;
    }
}
