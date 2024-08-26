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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);

        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out ISelectable component))
            {
                if (_currentSelect == null && _currentSelect != component && Input.GetMouseButtonDown(0))
                {
                    component.Deselect();
                    _currentSelect = component;
                    component.Select();
                }
            }
            else
            {
                CheckCurrentSelect();
            }
        }
        else
        {
            CheckCurrentSelect();
        }
    }

    private void CheckCurrentSelect()
    {
        if (_currentSelect != null && Input.GetMouseButtonDown(1))
        {
            _currentSelect.Deselect();
            _currentSelect = null;
        }
    }
}
