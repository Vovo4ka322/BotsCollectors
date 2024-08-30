using System.Collections.Generic;

public class ResourceDataBase
{
    private Dictionary<Resource, ResourceDataBaseCell> _cells = new();

    public void Add(IEnumerable<Resource> resources)
    {
        foreach(var resource in resources)
        {
            if (_cells.ContainsKey(resource) == false)
                _cells.Add(resource, new ResourceDataBaseCell(resource));
        }
    }

    public void Remove(Resource resource)
    {
        _cells.Remove(resource);
    }

    public bool IsResourceBusy(Resource resource) => _cells[resource].IsBaseTarget;

    public void Reservate(Resource resource) => _cells[resource].Reservate();
}
