using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public void Move(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
