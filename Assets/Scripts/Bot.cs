using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _placeForSeat;
    [SerializeField] private Base _base;

    [field: SerializeField] public BotMover Mover { get; private set; }

    private Transform _target;
    private int _resourceLayer = 8;

    public bool IsCollecting => Resource != null;

    private bool IsResourceTaken;

    public Resource Resource { get; private set; }

    public bool IsBusy => _target != null || IsCollecting;

    public event Action<Vector3, Bot> Reached;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Resource resource))
        {
            if (resource != Resource)
                return;

            resource.transform.position = _placeForSeat.position;
            resource.transform.SetParent(_placeForSeat);
            IsResourceTaken = true;
            _target = _base.transform;

            return;
        }

        if (collider.TryGetComponent(out Flag flag))
        {
            if (_target == flag.transform)
            {
                flag.CallAction();
                Reached?.Invoke(flag.transform.position, this);
                this.transform.SetParent(_base.transform);
                _target = null;
            }
        }

        if (IsResourceTaken == false)
            return;

        if (collider.TryGetComponent(out Base @base))
        {
            @base.Take(Resource);
            Resource = null;
            _target = null;
            IsResourceTaken = false;
            @base.Accept(this);
        }
    }

    private void Update()
    {
        if (_target == null)
            return;

        Mover.Move(_target);
    }

    public void SetBase(Base @base)
    {
        _base = @base;
    }

    public void Collect(Resource resource)
    {
        Resource = resource;
        _target = resource.transform;
        resource.gameObject.layer = _resourceLayer;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
