using UnityEngine;

namespace Inputs.Characters.Scripts
{
    public class AnimatorMediator : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] SaidController _saidController;
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _saidController = GetComponent<SaidController>();
        }
        
        void Update()
        {
        
            _animator.SetBool("IsGame", _saidController.isGameOn);
            if(_saidController.isJump)
            { 
                _animator.SetTrigger("Jump");
                _saidController.isJump = false;
            }
        }
    }
}
