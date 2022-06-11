using System;
using UnityEngine;

namespace Inputs.Characters.Scripts
{
    public class AnimatorEventHandler : MonoBehaviour
    {
        public Animator PlayerAnimator;
        private SaidController _saidController;

        private void Start()
        {
            _saidController = GetComponentInParent<SaidController>();
            _saidController.OnJump += AnimateJump;
        }

        public void AnimateJump()
        {
            PlayerAnimator.SetTrigger("Jump");
            _saidController.IsJump = false;
        }
    }
}
