using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Inputs.Characters.Scripts
{
    public class SaidMovement : MonoBehaviour
    {
        [SerializeField] private List<Transform> roads;
        private float _whereIsGoing;
        private Tweener _tweener = null;
        private void Start()
        {
            GetComponent<SaidController>().OnMoveAction += Move;
        }

        private void Move(int road)
        {
            if (_tweener != null)
            {
                if (_tweener.IsPlaying())
                {
                    Debug.Log("kill");
                    _tweener.Pause();
                    transform.position = new Vector3(_whereIsGoing, transform.position.y, transform.position.z);
                    _whereIsGoing = roads[road].position.x;
                    transform.DOMoveX(_whereIsGoing, 0.25F);
                }   
                else
                {
                    Debug.Log("START");
                    _whereIsGoing = roads[road].position.x;
                    _tweener = transform.DOMoveX(_whereIsGoing, 0.25f);
                }
            }
            else
            {
                Debug.Log("START");
                _whereIsGoing = roads[road].position.x;
                _tweener = transform.DOMoveX(_whereIsGoing, 0.25f);
            }
            // _whereIsGoing = roads[road].position.x;
            // transform.DOMoveX(roads[road].position.x, 0.25f);
            // transform.position = new Vector3(roads[road].position.x, transform.position.y,transform.position.z);
        }
    }
}