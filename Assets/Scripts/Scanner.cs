using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _findingRadius;
    [SerializeField] private LayerMask _layerOfResource;

    private int _timeToFindResource = 1;

    public event Action<List<Resource>> Discovered;

    private void OnEnable()
    {
        StartCoroutine(SearchResource());
    }

    private IEnumerator SearchResource()
    {
        WaitForSeconds timeForFind = new WaitForSeconds(_timeToFindResource);

        while (enabled)
        {
            Detecte();

            yield return timeForFind;
        }
    }

    private void Detecte()
    {
        Collider[] colliders = Physics.
            OverlapSphere(transform.position, _findingRadius, _layerOfResource);

        List<Resource> resources = new();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Resource resource))
            {
                resources.Add(resource);
            }
        }

        Discovered?.Invoke(resources);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _findingRadius);
    }
}
