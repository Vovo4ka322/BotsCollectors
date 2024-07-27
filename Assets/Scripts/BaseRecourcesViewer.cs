using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseRecourcesViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _logAmount;
    [SerializeField] private TextMeshProUGUI _goldAmount;
    [SerializeField] private TextMeshProUGUI _stoneAmount;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.Taken += AmountChanged;
    }

    private void OnDisable()
    {
        _base.Taken -= AmountChanged;
    }

    private void AmountChanged(int gold, int log, int stone)
    {
        Change(_goldAmount, gold);
        Change(_logAmount, log);
        Change(_stoneAmount, stone);
    }

    private void Change(TextMeshProUGUI text, int value)
    {
        text.text = value.ToString();
    }
}
