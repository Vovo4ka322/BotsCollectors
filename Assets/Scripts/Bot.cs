using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _placeForSeat;

    [field: SerializeField] public BotMover BotMover { get; private set; }

    public Base Base { get; private set; }

    public bool IsCollecting => Resource != null;

    public bool IsResourceTaken { get; private set; }

    public Resource Resource { get; private set; }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Resource>(out Resource resource))
        {
            if (resource != Resource)
                return;

            resource.transform.position = _placeForSeat.position;
            resource.transform.SetParent(_placeForSeat);
            IsResourceTaken = true;

            return;
        }

        if (IsResourceTaken == false)
            return;

        if (collider.TryGetComponent<Base>(out Base @base))
        {
            @base.Take(Resource);
            Resource = null;
            IsResourceTaken = false;           
        }
    }

    public void SetBase(Base @base)
    {
        Base = @base;
    }

    public void Collect(Resource resource)
    {
        Resource = resource;
    }
}