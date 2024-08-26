using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Base : MonoBehaviour, ISelectable
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Material _baseColor;
    [SerializeField] private Button _button;
    [SerializeField] private FlagCreator _flagCreator;
    [SerializeField] private BaseSpawner _spawner;

    private bool isGoingToFlag;
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

    public void Init(IEnumerable<Slot> slots, BaseSpawner baseSpawner)
    {
        foreach (Slot slot in slots)
        {
            _resourcesStoragies.Add(slot.ResourceType, slot);
        }

        _spawner = baseSpawner;
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

        Destroy(resource.gameObject);
    }

    private void OnResourceFound(List<Resource> resources)
    {
        var isBisyResources = _bots.Where(bot => bot.IsCollecting).Select(bot => bot.Resource);

        resources = resources.Except(isBisyResources).ToList();

        while (_bots.Any(bot => bot.IsBusy == false) && resources.Count > 0)
        {
            Bot bot = _bots.First(bot => bot.IsBusy == false);

            Resource resource = resources.First();

            bot.Collect(resource);

            resources.Remove(resource);
        }
    }

    public void CreateNewBot()
    {
        if (IsEnough(_botPrice))
        {
            foreach (var storage in _resourcesStoragies.Values)
            {
                storage.DecreaseQuantity();
            }

            Bot newBot = Instantiate(_botPrefab, transform.position, Quaternion.identity);
            _bots.Add(newBot);
            newBot.SetBase(this);
            newBot.transform.SetParent(this.transform);
            Debug.Log("Бот куплен");
        }
        else
        {
            Debug.Log("No many");
        }
    }

    private void Change()
    {
        isGoingToFlag = false;
    }

    public void Accept(Bot bot)
    {
        if (_flagCreator.Flag != null)
        {
            if (_flagCreator.Flag.gameObject.activeInHierarchy)
            {
                if (isGoingToFlag == false)
                {
                    if (IsEnough(_basePrice))
                    {
                        foreach (var storage in _resourcesStoragies.Values)
                        {
                            for (int i = 0; i < _basePrice; i++)
                            {
                                storage.DecreaseQuantity();
                            }
                        }

                        bot.SetTarget(_flagCreator.Flag.transform);
                        isGoingToFlag = true;
                    }
                    else
                    {
                        Debug.Log("Недостаточно денег");
                    }
                }
            }
        }
    }

    private bool IsEnough(int amount)
    {
        foreach (var storage in _resourcesStoragies.Values)
        {
            if (storage.Quantity < amount)
                return false;
        }

        return true;
    }

    public void Select()
    {
        _canvas.gameObject.SetActive(true);
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void Deselect()
    {
        _canvas.gameObject.SetActive(false);
        GetComponent<Renderer>().material.color = _baseColor.color;
    }
}
