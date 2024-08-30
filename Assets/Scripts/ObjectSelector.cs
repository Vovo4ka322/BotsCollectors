using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    private ISelectable _currentSelect;

    private void Update()
    {
        UseRaycast();    
    }

    private void UseRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.collider.gameObject.TryGetComponent(out ISelectable component))
                {
                    if (_currentSelect == null && _currentSelect != component)
                    {
                        component.Deselect();
                        _currentSelect = component;
                        component.Select();
                    }

                    return;
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_currentSelect != null)
            {
                _currentSelect.Deselect();
                _currentSelect = null;
            }
        }
    }
}
