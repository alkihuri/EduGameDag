using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventHandler : MonoBehaviour
{
    public event Action onJumpEndEvent;
    public void OnJumpEnd()
    {
        Debug.Log("jump ended");
        onJumpEndEvent?.Invoke();
    }
}
