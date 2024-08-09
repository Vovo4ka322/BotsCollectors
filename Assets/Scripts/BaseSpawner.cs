using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    private IEnumerable<Storage> _storagies;

    private void Start()
    {
        Spawn(transform.position);
    }

    public void Init(IEnumerable<Storage> storagies)
    {
        _storagies = storagies;
    }

    private Base Spawn(Vector3 position)
    {
        Base @base = Instantiate(_basePrefab, position, Quaternion.identity);
        @base.Init(_storagies);
        return @base;
    }
}
