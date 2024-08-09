using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Bot[] _bots;

    private Dictionary<string, Storage> _resourcesStoragies = new();

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

    public void Init(IEnumerable<Storage> storagies)
    {
        foreach (Storage storage in storagies)
        {
            _resourcesStoragies.Add(storage.ResourceType, storage);
        }
    }

    public void Take(Resource resource)
    {
        _resourcesStoragies[resource.GetType().Name].IncreaseQuantity();

        Destroy(resource.gameObject);
    }

    private void OnResourceFound(Resource resource)
    {
        bool isResourceCollecting = _bots.Any(bot => bot.Resource == resource);

        if (isResourceCollecting)
            return;

        Bot bot = _bots.FirstOrDefault(bot => bot.IsCollecting == false);

        if (bot == null)
            return;

        bot.Collect(resource);
    }
}
