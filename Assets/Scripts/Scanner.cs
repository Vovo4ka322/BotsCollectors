using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _findingRadius;
    [SerializeField] private LayerMask _layerOfResource;

    private int _timeToFindResource = 1;

    public event Action<List<Resource>> Found;

    private void OnEnable()
    {
        StartCoroutine(Detecter());
    }

    private IEnumerator Detecter()
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, _findingRadius, _layerOfResource);

        List<Resource> resources = new List<Resource>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Resource>(out Resource resource))
            {
                if (resources.Contains(resource) == false)
                    resources.Add(resource);
            }
        }

        Found?.Invoke(resources);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _findingRadius);
    }
}
