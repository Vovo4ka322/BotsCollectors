using UnityEngine;

public class BaseView : MonoBehaviour, ISelectable
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _baseColor;

    public void Select()
    {
        _canvas.gameObject.SetActive(true);
        _renderer.material.color = Color.red;
    }

    public void Deselect()
    {
        _canvas.gameObject.SetActive(false);
        _renderer.material.color = _baseColor;
    }
}
