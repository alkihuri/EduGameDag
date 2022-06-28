using GameCore.QuestPrefabs;
using System;
using System.Collections;
using UnityEngine;

namespace Inputs.Characters.Scripts
{
    public class SaidController : MonoBehaviour
    {
        public static SaidController Instance;
        public event Action<int> OnMoveAction;
        public event Action<int> OnMoveChanged;
        public bool isGameOn;

        public bool IsJump { get; set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        [SerializeField]
        private GameObject particle;


        [SerializeField][Range(0, 4)]
        private int currentRoad;


        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(0.1f);
            isGameOn = true;
        }


        private void OnGround() => particle.SetActive(true);

        private void OnMove(int direction)
        {
            OnMoveChanged?.Invoke(direction);
            var newRaod = currentRoad + direction;
            if (newRaod < 4 && newRaod > -1)
            {
                currentRoad = newRaod;
                IsJump = true;
                particle.SetActive(false);
                OnMoveAction?.Invoke(currentRoad);
            }
        }

        public void OnLeftMove() => OnMove(-1);

        public void OnRightMove() => OnMove(1);

        // private void OnTriggerEnter(Collider other)
        // {
        //     if(other.gameObject.GetComponent<AnswerObjectController>())
        //         OnJump?.Invoke();
        // }
    }
}
