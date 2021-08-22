using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackGizmoDrawer : MonoBehaviour
{
    [SerializeField] 
    private Vector3 center;
    
    [SerializeField]     
    private Vector3 size;

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 0.4f);
        center = transform.position;
        Gizmos.DrawCube(center,size);
    }
}
