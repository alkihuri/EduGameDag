using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SaidController : MonoBehaviour
{
    public event Action OnJump;
    public event Action<int> OnMoveAction;
    public bool isGameOn;
    public bool isJump;

    [SerializeField]
    private GameObject particle;

    [FormerlySerializedAs("_animatorEventHandler")]
    [SerializeField]
    private AnimatorEventHandler animatorEventHandler;

    [SerializeField, Range(0, 4)]
    int currentRoad;


    void Start()
    {
        StartCoroutine("StartGame");
        animatorEventHandler.onJumpEndEvent += OnGround;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        isGameOn = true;
    }


    private void OnGround()
    {
        particle.SetActive(true);
        isJump = false;
    }

    void OnMove(int direction)
    {
        int newRaod = currentRoad + direction;
        if (newRaod < 4 && newRaod > -1)
        {
            currentRoad = newRaod;
            isJump = true;
            particle.SetActive(false);
            OnJump?.Invoke();
            OnMoveAction?.Invoke(currentRoad);
        }
    }

    public void OnLeftMove()
    {
        OnMove(-1);
    }

    public void OnRightMove()
    {
        OnMove(1);
    }
}