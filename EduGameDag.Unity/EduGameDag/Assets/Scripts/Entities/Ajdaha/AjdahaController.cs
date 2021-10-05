using UnityEngine;

namespace Entities.Ajdaha
{
    public class AjdahaController : MonoBehaviour
    {

        public bool isAttack, isRun, isIdle =  true;
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

        public void LetAjdahaRun()
        {
            _animatorOfAjdaha.SetBool("isRun", true);
            _animatorOfAjdaha.SetBool("isIdle", false);
        }
    }
}
