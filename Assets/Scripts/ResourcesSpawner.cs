using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    private Resource[] _resources;

    private int _randomNumber;
    private int _timeToSpawn = 5;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    public void Spawn()
    {
        Instantiate(_resources[CreateIndex(_resources)], _spawnPoints[CreateIndex(_spawnPoints)].position, Quaternion.identity);
    }

    public void Init(Resource[] resources)
    {
        _resources = resources;
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds timeToSpawn = new(_timeToSpawn);

        while (enabled)
        {
            Spawn();

            yield return timeToSpawn;
        }
    }

    private int CreateIndex<T>(T[] array)
    {
        _randomNumber = UnityEngine.Random.Range(0, array.Length);

        return _randomNumber;
    }
}