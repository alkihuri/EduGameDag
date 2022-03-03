using GameCore.QuestPrefabs;
using UnityEngine;

namespace Entities.Ajdaha
{
    public class AjdahaController : MonoBehaviour
    {

        public bool isAttack, isRun, isIdle =  true, isJump;
        [SerializeField] Animator _animatorOfAjdaha; 
        void Start()
        {
            _animatorOfAjdaha = GetComponentInChildren<Animator>();
            LetAjdahaRun();
        }

        private void JumpAjdaha()
        {
            _animatorOfAjdaha.SetTrigger("isJump");
            isJump = true;
            LetAjdahaRun();
        }

        private void LetAjdahaRun()
        {
            isJump = false; 
            _animatorOfAjdaha.SetBool("isRun", true);
            _animatorOfAjdaha.SetBool("isIdle", false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<AnswerObjectController>())
                JumpAjdaha();
        }
    }
}
