using UnityEngine;
using TMPro;

public class BaseRecourcesViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourceAmount;

    private Slot _storage;

    private void OnEnable()
    {
        _storage.AmountChanged += OnChanged;
    }

    private void OnDisable()
    {
        if (_storage != null)
            _storage.AmountChanged -= OnChanged;
    }

    public void Init(Slot storage)
    {
        _storage = storage;
    }

    private void OnChanged(int value)
    {
        _resourceAmount.text = value.ToString();
    }
}
