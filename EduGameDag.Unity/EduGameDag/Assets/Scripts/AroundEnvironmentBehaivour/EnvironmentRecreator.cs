using System.Collections;
using System.Collections.Generic;
using AroundEnvironmentBehaivour;
using UnityEngine;

public class EnvironmentRecreator : MonoBehaviour
{
    [SerializeField]
    private EnvironmentMovement _environmentMovement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PackGizmoDrawer>()!=null)
        _environmentMovement.RecreateEnvironment(other.gameObject.transform);
    }
}
