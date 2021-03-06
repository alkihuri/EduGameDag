using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour
{
    [SerializeField, Range(-2, 4)] float _forceValue;
    // Start is called before the first frame update  
    // Update is called once per frame
    void Update()
    {
        _forceValue = UnityEngine.Random.Range(-1, 4);
        GetComponent<Rigidbody>().AddForce(transform.up * _forceValue + transform.right* UnityEngine.Random.Range(-1,1));
    }

    public  void BreakSpring()
    {
        Destroy(GetComponent<SpringJoint>());
        GetComponent<Rigidbody>().AddForce(transform.up * 3.5f, ForceMode.Impulse); 
        Destroy(gameObject, 5);
    }
}
