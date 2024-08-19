using UnityEngine;

public class ShowOutlineMaterial : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private Material _outlineMaterial;
    private Material _originalMaterial;
    private bool _isHighlighted;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;
        _isHighlighted = false;
    }

    public void ChangeToHighlightMaterial()
    {
        if (!_isHighlighted)
        {
            _renderer.material = _outlineMaterial;
            _isHighlighted = true;
            Debug.Log("Changed to highlight material");
        }
    }

    public void ChangeToOriginalMaterial()
    {
        if (_isHighlighted)
        {
            _renderer.material = _originalMaterial;
            _isHighlighted = false;
            Debug.Log("Changed to original material");
        }
    }
}
