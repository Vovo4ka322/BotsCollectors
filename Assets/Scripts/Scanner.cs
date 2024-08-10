using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]private float _findingRadius;
    [SerializeField]private LayerMask _resource;

    private int _timeToFindResource = 1;

    public event Action<List<Resource>> Found;

    private void Start()
    {
        StartCoroutine(Detecter());
    }

    private IEnumerator Detecter()
    {
        WaitForSeconds timeForFind = new(_timeToFindResource);

        while (enabled)
        {
            Detecte();

            yield return timeForFind;
        }
    }
    
    private void Detecte()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _findingRadius, _resource);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Resource>(out Resource resource))
            {
                List<Resource> resources = new()
                {
                    resource
                };

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
