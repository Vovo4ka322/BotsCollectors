using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Base _emptyBase;

    private ResourceViewerHolder _resourceViewerHolderPrefab;

    private IEnumerable<Slot> _storagies;

    public void Init(IEnumerable<Slot> storagies, ResourceViewerHolder resourceViewerHolderPrefab)
    {
        _storagies = storagies;
        _resourceViewerHolderPrefab = resourceViewerHolderPrefab;
        Spawn(transform.position, _basePrefab);
    }

    public Base SpawnEmptyBase(Vector3 position, Bot bot)
    {
        Base @base = Spawn(position, _emptyBase);
        @base.SetBot(bot);
        bot.transform.SetParent(@base.transform);
        return @base;
    }

    public Base Spawn(Vector3 position, Base basePrefab)
    {
        Base @base = Instantiate(basePrefab, position, Quaternion.identity);
        var newStoragies = _storagies.Select(storage => storage.Clone()).ToList();
        ResourceViewerHolder resourceViewerHolder = Instantiate(_resourceViewerHolderPrefab, position, _resourceViewerHolderPrefab.transform.rotation);

        IReadOnlyList<BaseRecourcesViewer> baseRecourcesViewers = resourceViewerHolder.BaseRecourcesViewers;

        for (int i = 0; i < resourceViewerHolder.BaseRecourcesViewers.Count(); i++)
        {
            baseRecourcesViewers[i].Init(newStoragies[i]);
        }

        @base.Init(newStoragies, this);
        return @base;
    }
}
