using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour
{
    [SerializeField, Range(-2, 4)] float _forceValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _forceValue = UnityEngine.Random.Range(-1, 4);
        GetComponent<Rigidbody>().AddForce(transform.up * _forceValue);
    }

    public  void BreakSpring()
    {
        Destroy(GetComponent<SpringJoint>());
        Destroy(gameObject, 5);
    }
}