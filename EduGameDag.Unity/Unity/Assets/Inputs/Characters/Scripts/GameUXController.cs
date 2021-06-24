using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUXController : MonoBehaviour
{
    [SerializeField] SaidController _saidController;
    private void Start()
    {
        _saidController = GetComponent<SaidController>();
    }
    // Update is called once per frame
    private void OnMouseDrag()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.deltaPosition.x > 0f)
        {
            _saidController.OnRightMove();
            Debug.Log("right");
        }
        if (touch.deltaPosition.x < 0f)
        {
            _saidController.OnLeftMove();
            Debug.Log("left");
        }
    }
}
