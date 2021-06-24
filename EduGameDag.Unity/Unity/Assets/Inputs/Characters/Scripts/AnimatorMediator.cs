using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMediator : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] SaidController _saidController;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _saidController = GetComponent<SaidController>();
    }
    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("Jump", _saidController.isJump); 
        _animator.SetBool("IsGame", _saidController.isGameOn);
    }
}
