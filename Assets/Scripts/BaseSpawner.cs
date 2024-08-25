using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Base _emptyBase;

    private IEnumerable<Slot> _storagies;

    public void Init(IEnumerable<Slot> storagies)
    {
        _storagies = storagies;
        SpawnFullBase(transform.position);
    }

    public Base SpawnFullBase(Vector3 position)
    {
        Base @base;
        Spawn(position, out @base, _basePrefab);
        return @base;
    }

    public Base SpawnEmptyBase(Vector3 position, Bot bot)
    {
        Base @base;
        Spawn(position,out @base, _emptyBase);
        @base.SetBot(bot);
        return @base;
    }

    public Base Spawn(Vector3 position, out Base @base, Base basePrefab)
    {
        @base = Instantiate(basePrefab, position, Quaternion.identity);
        @base.Init(_storagies, this);
        return @base;
    }
}
