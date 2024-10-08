using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private Resource[] _resources;
    private int _timeToSpawn = 5;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public void Init(Resource[] resources)
    {
        _resources = resources;
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new(_timeToSpawn);

        while (enabled)
        {
            Instantiate(_resources[UnityEngine.Random.Range(0, _resources.Length)],
            _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);

            yield return wait;
        }
    }
}