using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseRecourcesViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourceAmount;

    private Storage _storage;

    private void Start()
    {
        _storage.AmountChanged += OnChanged;
    }

    private void OnDestroy()
    {
        _storage.AmountChanged -= OnChanged;
    }

    public void Init(Storage storage)
    {
        _storage = storage;
    }

    private void OnChanged(int value)
    {
        _resourceAmount.text = value.ToString();
    }
}
