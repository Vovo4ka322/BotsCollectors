using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Bot _bot;

    private void Update()
    {
        if (_bot.IsCollecting == false)
            return;

        if (_bot.IsResourceTaken)
            MoveToBase();
        else
            MoveToResource();
    }

    public void MoveToResource()
    {
        transform.position = Vector3.MoveTowards(transform.position, _bot.Resource.transform.position, _moveSpeed * Time.deltaTime);
    }

    public void MoveToBase()
    {
        transform.position = Vector3.MoveTowards(transform.position, _bot.Base.transform.position, _moveSpeed * Time.deltaTime);
    }
}
