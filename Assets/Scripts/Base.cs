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

        _scanner.Clear(resource);
    }

    private void OnResourceFound(List<Resource> resources)
    {
        Bot bot = _bots.FirstOrDefault(bot => bot.IsCollecting == false);

        Resource resource = resources.FirstOrDefault(resource => resource.IsFree);

        if (bot == null)
            return;

        if (resource == null)
            return;

        bot.Collect(resource);

        resource.Change();
    }
}
