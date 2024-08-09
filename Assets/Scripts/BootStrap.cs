using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private ResourcesSpawner _resourcesSpawner;
    
    [SerializeField] private BaseRecourcesViewer[] _resourceVoiewers;
    [SerializeField] private Resource[] _resources;

    private void OnValidate()
    {
        if (_resources.Length != _resourceVoiewers.Length)
            throw new Exception();
    }

    private void Awake()
    {
        Storage[] storagies = new Storage[_resources.Length];

        for (int i = 0; i < _resources.Length; i++)
        {
            Storage storage = new(_resources[i]);
            _resourceVoiewers[i].Init(storage);

            storagies[i] = storage;          
        }

        _resourcesSpawner.Init(_resources);
        _baseSpawner.Init(storagies);
    }
}
