using System;

public class Slot
{
    private Resource _resource;

    public Slot(Resource resource)
    {
        _resource = resource;
        ResourceType = _resource.GetType().Name;
    }

    public Slot Clone()
    {
        return new Slot(_resource);
    }

    public event Action<int> AmountChanged;

    public int Quantity { get; private set; }

    public string ResourceType { get; private set; }

    public void IncreaseQuantity(int amount = 1)
    {
        if (amount < 0)
            return;

        Quantity += amount;
        AmountChanged?.Invoke(Quantity);
    }

    public void DecreaseQuantity(int amount = 1)
    {
        if (amount < 0)
            return;

        Quantity -= amount;
        AmountChanged?.Invoke(Quantity);
    }    
}
