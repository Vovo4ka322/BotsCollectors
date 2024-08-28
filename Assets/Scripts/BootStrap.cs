using System;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private ResourcesSpawner _resourcesSpawner;
    [SerializeField] private ResourceViewerHolder _resourceViewerHolderPrefab;
    [SerializeField] private Resource[] _resources;

    private void OnValidate()
    {
        if (_resources.Length != _resourceViewerHolderPrefab.BaseRecourcesViewers.Count())
            throw new Exception();
    }

    private void Awake()
    {
        Slot[] storagies = new Slot[_resources.Length];        

        for (int i = 0; i < _resources.Length; i++)
        {
            Slot storage = new Slot(_resources[i]);

            storagies[i] = storage;          
        }

        _resourcesSpawner.Init(_resources);
        _baseSpawner.Init(storagies, _resourceViewerHolderPrefab);
    }
}
