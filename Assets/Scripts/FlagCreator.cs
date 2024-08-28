using UnityEngine;
using UnityEngine.UI;

public class FlagCreator : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskPlatform;

    [field:SerializeField] public Flag Flag { get; private set; }

    private Camera _camera;
    private Ray _ray;


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Flag.gameObject.activeSelf == false)
        {
            return;
        }

        UpdateFlagPosition();
        
        SetFlag();
    }

    public void Activate()
    {
        enabled = true;

        Flag.Activate();
    }

    public void Deactivate()
    {
        enabled = false;
    }

    public void UpdateFlagPosition()
    {
        if (Flag == null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out Platform _))
        {
            Flag.transform.position = hit.point;
        }
    }

    public void SetFlag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, float.MaxValue, _layerMaskPlatform) && hit.collider.TryGetComponent(out Platform platform))
            {
                Flag.Activate();
                Deactivate();
            }
        }
    }
}
