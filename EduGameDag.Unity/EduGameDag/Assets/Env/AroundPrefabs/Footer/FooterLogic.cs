using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooterLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 2, (float)Random.Range(2000, 3000) / 1000);
    }
     
}
