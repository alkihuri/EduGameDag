using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBinder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PartController>())
            return;
         
 
    }
}
