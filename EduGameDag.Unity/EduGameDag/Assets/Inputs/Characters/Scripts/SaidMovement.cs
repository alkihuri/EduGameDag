using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Inputs.Characters.Scripts;

public class SaidMovement : MonoBehaviour
{
    [SerializeField]
    List<Transform> roads;
    Sequence currSequense;
    private void Start()
    {
        GetComponent<SaidController>().OnMoveAction += Move;
    }

    private void Move(int road)
    {
        // transform.DOMoveX(roads[road].position.x, 0.25f);
        transform.position = new Vector3(roads[road].position.x, transform.position.y,transform.position.z);
    }
}
