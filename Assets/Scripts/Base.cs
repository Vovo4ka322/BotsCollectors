using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Bot[] _bots;

    private Dictionary<string, Slot> _resourcesStoragies = new();

    private void Start()
    {
        foreach (Bot bot in _bots)
        {
            bot.SetBase(this);
        }
    }

    private void OnEnable()
    {
        _scanner.Found += OnResourceFound;
    }

    private void OnDisable()
    {
        _scanner.Found -= OnResourceFound;
    }

    public void Init(IEnumerable<Slot> storagies)
    {
        foreach (Slot storage in storagies)
        {
            _resourcesStoragies.Add(storage.ResourceType, storage);
        }
    }

    public void Take(Resource resource)
    {
        _resourcesStoragies[resource.GetType().Name].IncreaseQuantity();

        Destroy(resource.gameObject);
    }

    private void OnResourceFound(List<Resource> resources)
    {
        var isBisyResources = _bots.Where(bot => bot.IsCollecting).Select(bot => bot.Resource);

        resources = resources.Except(isBisyResources).ToList();

        while (_bots.Any(bot => bot.IsCollecting == false) && resources.Count > 0)
        {
            Bot bot = _bots.First(bot => bot.IsCollecting == false);

            Resource resource = resources.First();

            bot.Collect(resource);

            resources.Remove(resource);
        }
    }
}
