using GameCore.QuestPrefabs;
using UnityEngine;

namespace Entities.Ajdaha
{
    public class AjdahaController : MonoBehaviour
    {

        public bool isAttack, isRun, isIdle =  true, isJump;
        [SerializeField] Animator _animatorOfAjdaha; 
        // Start is called before the first frame update
        void Start()
        {
            _animatorOfAjdaha = GetComponentInChildren<Animator>();
            LetAjdahaRun();
        }

        // Update is called once per frame
        void Update()
        {
      
        }
        public void JumpAjdaha()
        {
            _animatorOfAjdaha.SetTrigger("isJump");
            isJump = true;
          //  GetComponent<Rigidbody>().AddForce(transform.up * 3, ForceMode.Impulse);
            LetAjdahaRun();
        }
        public void LetAjdahaRun()
        {
            isJump = false; 
            //_animatorOfAjdaha.ResetTrigger("isJump");
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
