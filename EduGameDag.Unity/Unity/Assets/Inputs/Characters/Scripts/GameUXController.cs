using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUXController : MonoBehaviour
{
    [SerializeField] SaidController _saidController;
    Touch touch;
    private void Start()
    {
        _saidController = GetComponent<SaidController>();
    }
    // Update is called once per frame
    private void Update()
    {
       // touch = Input.GetTouch(0); 
        if (touch.deltaPosition.x > 0f || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _saidController.OnRightMove();
            Debug.Log("right");
        }
        if (touch.deltaPosition.x < 0f || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _saidController.OnLeftMove();
            Debug.Log("left");
        }
    }
}
