﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SaidController : MonoBehaviour
{
    private event Action OnJump;
    public bool isGameOn;
    public bool isJump;

    [SerializeField]
    private GameObject particle;

    [FormerlySerializedAs("_animatorEventHandler")]
    [SerializeField]
    private AnimatorEventHandler animatorEventHandler;

    [SerializeField, Range(0, 10)]
    float speedOftransition = 5;

    [SerializeField, Range(0, 4)]
    int currentRoad;

    [SerializeField]
    List<Transform> roads; // have to be serialized

    // Start is called before the first frame update
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

    void Update()
    {
        if (isGameOn)
            transform.position = Vector3.MoveTowards(transform.position,
                roads[currentRoad].position + new Vector3(0, 1, 0), speedOftransition / 10);
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
        }
        else
            Debug.Log("Error!");
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