using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUXController : MonoBehaviour
{
    [SerializeField] SaidController _saidController;
    private Vector2 startTouchPosition;
    [SerializeField] private float touchThreshold = 20f;
    private void Start()
    {
        _saidController = GetComponent<SaidController>();
    }
    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            WorkWithTouches();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _saidController.OnRightMove();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _saidController.OnLeftMove();
        }
    }
    void WorkWithTouches()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            startTouchPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (Mathf.Abs(touch.position.x - startTouchPosition.x) > touchThreshold)
            {
                if ((touch.position.x - startTouchPosition.x) > 0)
                {
                    _saidController.OnRightMove();
                }
                else
                {
                    _saidController.OnLeftMove();
                }
            }
        }
    }
}
