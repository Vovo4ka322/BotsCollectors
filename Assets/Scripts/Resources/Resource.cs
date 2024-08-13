using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsFree { get; private set; } = true;

    public void Change()
    {
        IsFree = false;
    }
}
