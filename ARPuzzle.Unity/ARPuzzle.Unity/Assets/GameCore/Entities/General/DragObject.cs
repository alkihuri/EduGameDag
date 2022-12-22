using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DragObject : MonoBehaviour
{
    private const int SELECTED_Y_LEVEL = 5;
    private const int REALESED_Y_LEVEL = 1;
    private const float ANIMATION_DURATION = 0.1f;
    private const int OBJECT_SIZE = 10;

    [SerializeField] private GameObject _shadow;


    private Vector3 _mOffset;
    private float _mZCoord;
    private Camera _mainCamera;
    private Vector3 _calculatedPostion;

    public bool Is2D { get; private set; }

    private void Awake()
    {
        _mainCamera = Camera.main;
        Is2D = true;
    }

    void OnMouseDown()
    {
        _mZCoord = _mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        _mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private void OnMouseExit()
    {


        transform.DOMove(_calculatedPostion, ANIMATION_DURATION);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mZCoord;
        return _mainCamera.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        var x = (GetMouseAsWorldPoint().x + _mOffset.x);
        var y = (GetMouseAsWorldPoint().y + _mOffset.y);
        var z = (GetMouseAsWorldPoint().z + _mOffset.z);

        Vector3 newPosition = new Vector3(x, y, z);

        transform.DOMove(newPosition, ANIMATION_DURATION);
    }

    private void OnMouseEnter()
    {
        transform.DOMoveY(SELECTED_Y_LEVEL, ANIMATION_DURATION);
    }
    private void OnMouseOver()
    {
        OnHoverOnPart();
    }

    private void OnHoverOnPart()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;

        x -= (x % OBJECT_SIZE);
        y -= (y % OBJECT_SIZE);
        z -= (z % OBJECT_SIZE);

        y = Is2D ? 0 : y;

        _calculatedPostion = new Vector3(x, y, z);

        _shadow.transform.DOMove(new Vector3(x, 0.1f, z), 0.05f);
    }
}