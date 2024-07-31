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
            Move(_bot.Base.transform);       
        else
            Move(_bot.Resource.transform);
    }

    private void Move(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
    }
}
