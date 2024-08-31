using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Material _baseColor;
    [SerializeField] private FlagCreator _flagCreator;
    [SerializeField] private Renderer _renderer;

    private ResourceDataBase _resourceDataBase;
    private BaseSpawner _spawner;
    private bool isBotComeToFlag;
    private int _botPrice = 1;
    private int _basePrice = 5;
    private Dictionary<string, Slot> _resourcesStoragies = new();

    private void OnEnable()
    {
        _scanner.Discovered += OnResourceFound;
        _flagCreator.Flag.Reached += Change;

        foreach (Bot bot in _bots)
        {
            bot.SetBase(this);
            bot.Reached += CreateNewBase;
        }
    }

    private void OnDisable()
    {
        _scanner.Discovered -= OnResourceFound;
        _flagCreator.Flag.Reached -= Change;

        foreach (Bot bot in _bots)
        {
            bot.Reached -= CreateNewBase;
        }
    }

    public void Init(IEnumerable<Slot> slots, BaseSpawner baseSpawner, ResourceDataBase resourceDataBase)
    {
        foreach (Slot slot in slots)
        {
            _resourcesStoragies.Add(slot.ResourceType, slot);
        }

        _spawner = baseSpawner;
        _resourceDataBase = resourceDataBase;
    }

    public void SetBot(Bot bot)
    {
        bot.SetBase(this);
        _bots.Add(bot);
    }

    public void CreateNewBase(Vector3 position, Bot bot)
    {
        _spawner.SpawnEmptyBase(position, bot);
    }

    public void Take(Resource resource)
    {
        _resourcesStoragies[resource.GetType().Name].IncreaseQuantity();
        _resourceDataBase.Remove(resource);
        Destroy(resource.gameObject);
    }

    private void OnResourceFound(List<Resource> resources)
    {
        _resourceDataBase.Add(resources);

        foreach (Resource resource in resources)
        {
            if (_resourceDataBase.IsResourceBusy(resource) == false)
            {
                if (TryGetBot(out Bot bot) == false)
                    return;

                bot.Collect(resource);
                _resourceDataBase.Reservate(resource);
            }
        }
    }

    private bool TryGetBot(out Bot bot)
    {
        bot = _bots.FirstOrDefault(@bot => @bot.IsBusy == false);

        return bot != null;
    }

    public void CreateNewBot()
    {
        if (IsEnough(_botPrice))
        {
            foreach (var storage in _resourcesStoragies.Values)
                storage.DecreaseQuantity(_botPrice);

            Bot newBot = Instantiate(_botPrefab, transform.position, Quaternion.identity);
            _bots.Add(newBot);
            newBot.SetBase(this);
            newBot.transform.SetParent(transform);
        }
    }

    private void Change()
    {
        isBotComeToFlag = false;
    }

    public void Accept(Bot bot)
    {
        if (_flagCreator.Flag == null)
            return;

        if (_flagCreator.Flag.gameObject.activeInHierarchy == false)
            return;

        if (isBotComeToFlag)
            return;

        if (IsEnough(_basePrice) == false)
            return;

        foreach (var storage in _resourcesStoragies.Values)
            storage.DecreaseQuantity(_basePrice);

        bot.SetTarget(_flagCreator.Flag.transform);
        isBotComeToFlag = true;
    }

    private bool IsEnough(int amount)
    {
        foreach (var storage in _resourcesStoragies.Values)
            if (storage.Quantity < amount)
                return false;

        return true;
    }
}
