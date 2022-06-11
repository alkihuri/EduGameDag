using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Inputs.Characters.Scripts
{
    public class SaidMovement : MonoBehaviour
    {
        [SerializeField] private List<Transform> roads;
        private float _whereIsGoing;
        private void Start() => GetComponent<SaidController>().OnMoveAction += Move;

        private void Move(int road)
        {
            _whereIsGoing = roads[road].position.x;
            transform.DOMoveX(_whereIsGoing, 0.2f);
        }
    }
}
