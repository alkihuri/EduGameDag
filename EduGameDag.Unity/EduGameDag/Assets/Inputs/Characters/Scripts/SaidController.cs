using GameCore.QuestPrefabs;
using System;
using System.Collections;
using UnityEngine;

namespace Inputs.Characters.Scripts
{
    public class SaidController : MonoBehaviour
    {
        public static SaidController instance;
        public event Action OnJump;
        public event Action<int> OnMoveAction;
        public bool isGameOn;

        public bool IsJump { get; set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        [SerializeField]
        private GameObject particle;

    
        public AnimatorEventHandler animatorEventHandler;
        public SaidMovement _saidMovement;

        [SerializeField, Range(0, 4)]
        int currentRoad;


        private void Start()
        {
            StartCoroutine(StartGame());
            animatorEventHandler.onJumpEndEvent += OnGround;
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(0.1f);
            isGameOn = true;
        }


        private void OnGround() => particle.SetActive(true);

        private void OnMove(int direction)
        {
            var newRaod = currentRoad + direction;
            if (newRaod < 4 && newRaod > -1)
            {
                currentRoad = newRaod;
                IsJump = true;
                particle.SetActive(false);
                OnJump?.Invoke();
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
