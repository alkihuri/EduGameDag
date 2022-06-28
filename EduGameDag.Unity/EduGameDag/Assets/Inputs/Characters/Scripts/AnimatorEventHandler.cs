using UnityEngine;
using DG.Tweening;

namespace Inputs.Characters.Scripts
{
    public class AnimatorEventHandler : MonoBehaviour
    {
        private SaidController _saidController;

        private void Start()
        {
            _saidController = GetComponentInParent<SaidController>();
            _saidController.OnMoveChanged += AnimateJump;
        }

        private void AnimateJump(int obj)
        {
            if(obj == 1) transform.DORotate(Vector3.up * 50, 0.2f).OnComplete(() => transform.DORotate(Vector3.zero, 0.2f));
            else transform.DORotate(Vector3.down * 50, 0.2f).OnComplete(() => transform.DORotate(Vector3.zero, 0.2f));
            _saidController.IsJump = false;
        }
    }
}
