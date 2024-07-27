using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawners;
    [SerializeField] private Transform[] _resources;

    private int _index;
    private int _oneMinute = 5;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds minute = new(_oneMinute);

        while (enabled)
        {
            Spawn();

            yield return minute;
        }
    }

    public void Spawn()
    {
        Instantiate(_resources[CreateIndex(_resources)], _spawners[CreateIndex(_spawners)].position, Quaternion.identity);
    }

    private int CreateIndex(Transform[] array)
    {
        _index = Random.Range(0, array.Length);

        return _index;
    }
}
