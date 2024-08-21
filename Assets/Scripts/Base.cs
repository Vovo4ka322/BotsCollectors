using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour, ISelectable
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Bot[] _bots;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Material _baseColor;

    private int _botPrice = 1;
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

    public void Init(IEnumerable<Slot> slots)
    {
        foreach (Slot slot in slots)
        {
            _resourcesStoragies.Add(slot.ResourceType, slot);
        }

        //var log = _resourcesStoragies.Keys.ToArray()[1];
        //Debug.Log(log);
        //Debug.Log(typeof(Log).Name);
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

    public void CreateNewBot()
    {
        if (IsEnough(_botPrice))
        {
            var keys = _resourcesStoragies.Keys.ToArray();

            foreach (var key in keys)
            {
                _resourcesStoragies[key].DecreaseQuantity();
            }
            
            Instantiate(_botPrefab, transform.position, Quaternion.identity);
            Debug.Log("Бот куплен");
        }
    }

    private bool IsEnough(int amount)
    {
        //if (_resourcesStoragies.ContainsKey(typeof(Log).BaseType.Name))
        //    Debug.Log("Содержится BaseType");

        //if (_resourcesStoragies.ContainsKey(typeof(Log).FullName))
        //    Debug.Log("Содержится Full Name");

        //if(_resourcesStoragies.ContainsKey(typeof(Log).Name))
        //    Debug.Log("Содержится Name");

        //var log = _resourcesStoragies.Keys.ToArray()[1];
        //Debug.Log(log);
        //Debug.Log(typeof(Log).Name);

        if (_resourcesStoragies[typeof(Log).BaseType.Name].Quantity >= amount && _resourcesStoragies[typeof(Gold).Name].Quantity >= amount && _resourcesStoragies[typeof(Stone).Name].Quantity >= amount)
            return true;

        Debug.Log("Не хватает ресов");
        return false;
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
