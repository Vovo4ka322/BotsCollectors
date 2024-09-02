using System;
using UnityEngine;

public class Flag : MonoBehaviour
{ 
    public event Action Reached;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void CallReachedAction()
    {
        Deactivate();
        Reached?.Invoke();
    }
}
