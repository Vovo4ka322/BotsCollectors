using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    private Resource _resource;

    public Storage(Resource resource)
    {
        _resource = resource;
    }

    public event Action<int> AmountChanged;

    public int Quantity { get; private set; }

    public string ResourceType => _resource.GetType().Name;

    public void IncreaseQuantity()
    {
        Quantity++;
        AmountChanged?.Invoke(Quantity);
    }

    public void DecreaseQuantity()
    {
        Quantity--;
        AmountChanged?.Invoke(Quantity);
    }
}
