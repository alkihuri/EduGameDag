using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] GameObject _particleSystem;

    private void OnDestroy()
    {
        GameObject  ps = Instantiate(_particleSystem,transform.position,Quaternion.identity);
        Destroy(ps, 3);
    }
}
