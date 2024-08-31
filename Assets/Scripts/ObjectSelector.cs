using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    private int _selectCommand = 0;
    private int _deselectCommand = 1;
    private ISelectable _currentSelect;

    private void Update()
    {
        UseRaycast();    
    }

    private void UseRaycast()
    {
        if (Input.GetMouseButtonDown(_selectCommand))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.collider.gameObject.TryGetComponent(out ISelectable component))
                {
                    if (_currentSelect != component)
                    {
                        component.Deselect();
                        _currentSelect = component;
                        component.Select();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(_deselectCommand))
        {
            if (_currentSelect != null)
            {
                _currentSelect.Deselect();
                _currentSelect = null;
            }
        }
    }
}
