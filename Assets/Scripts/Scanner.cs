using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]private float _findingRadius;

    public event Action<Resource> Found;

    private void Update()
    {
        Detecte();
    }

    private void Detecte()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _findingRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Resource>(out Resource resources))
            {
                Found?.Invoke(resources); 
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _findingRadius);
    }
}
