using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Bot[] _bots;
    [SerializeField] private List<Bot> _activeBots;

    private int _amount = 1;

    public int GoldValue {  get; private set; }

    public int LogValue { get; private set; }

    public int StoneValue { get; private set; }


    public event Action<int, int, int> Taken;

    private void Start()
    {
        foreach(Bot bot in _bots)
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

    public void Take(Resource resource)
    {
        if (resource.GetComponent<Log>())
            TakeLog(_amount);

        if (resource.GetComponent<Stone>())
            TakeStone(_amount);

        if (resource.GetComponent<Gold>())
            TakeGold(_amount);

        Destroy(resource.gameObject);
    }

    private void TakeGold(int amount)
    {
        GoldValue += amount;
        Taken?.Invoke(GoldValue, LogValue, StoneValue);
    }

    private void TakeLog(int amount)
    {
        LogValue += amount;
        Taken?.Invoke(GoldValue, LogValue, StoneValue);
    }

    private void TakeStone(int amount)
    {
        StoneValue += amount;
        Taken?.Invoke(GoldValue, LogValue, StoneValue);
    }

    private void OnResourceFound(Resource resource)
    {
        var isResourceCollecting = _bots.Any(bot => bot.Resource == resource);

        if (isResourceCollecting)
            return;

        var bot = _bots.FirstOrDefault(bot => bot.IsCollecting == false);

        if (bot == null)
            return;

        bot.Collect(resource);
    }
}
