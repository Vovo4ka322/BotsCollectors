using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _placeForSeat;
    [SerializeField] private Base _base;
    [SerializeField] public BotMover Mover;

    private Transform _target;
    private bool _isResourceTaken;

    public event Action<Vector3, Bot> Reached;

    public bool IsCollecting => Resource != null;

    public bool IsBusy => _target != null || IsCollecting;

    public Resource Resource { get; private set; }

    private void OnTriggerEnter(Collider collider)
    {
        TryGetComponentResource(collider);

        TryGetComponentFlag(collider);

        if (_isResourceTaken == false)
            return;

        TryGetComponentBase(collider);
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
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void TryGetComponentResource(Collider collider)
    {
        if (collider.TryGetComponent(out Resource resource) && resource == Resource)
        {
            resource.transform.position = _placeForSeat.position;
            resource.transform.SetParent(_placeForSeat);
            resource.TurnOnKinematic();
            _isResourceTaken = true;
            _target = _base.transform;
        }
    }

    private void TryGetComponentFlag(Collider collider)
    {
        if (collider.TryGetComponent(out Flag flag))
        {
            if (_target == flag.transform)
            {
                flag.CallReachedAction();
                Reached?.Invoke(flag.transform.position, this);
                _target = null;
            }
        }
    }

    private void TryGetComponentBase(Collider collider)
    {
        if (collider.TryGetComponent(out Base @base) && @base == _base)
        {
            @base.Take(Resource);
            Resource = null;
            _target = null;
            _isResourceTaken = false;
            @base.Accept(this);
        }
    }
}
