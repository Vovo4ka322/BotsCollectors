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
        Slot[] storagies = new Slot[_resources.Length];        

        for (int i = 0; i < _resources.Length; i++)
        {
            Slot storage = new Slot(_resources[i]);
            _resourceVoiewers[i].Init(storage);

            storagies[i] = storage;          
        }

        _resourcesSpawner.Init(_resources);
        _baseSpawner.Init(storagies);
    }
}
