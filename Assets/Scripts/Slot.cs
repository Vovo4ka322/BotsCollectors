using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    private Resource _resource;

    public Slot(Resource resource)
    {
        _resource = resource;
        ResourceType = _resource.GetType().Name;
    }

    public event Action<int> AmountChanged;

    public int Quantity { get; private set; }

    public string ResourceType;

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
