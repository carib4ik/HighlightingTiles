using UnityEngine;


public class CursorPoint : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Color _pointedColor;
    
    private Renderer _lastObjectRenderer;
    private Color _originalColor;

    private void Update()
    {
        ColoringPointedObject();
    }

    private void ColoringPointedObject()
    {
        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, 100f, _layer))
        {
            var currentRenderer = hitInfo.collider.GetComponent<Renderer>();
            
            if (_lastObjectRenderer != currentRenderer && _lastObjectRenderer is not null)
            {
                _lastObjectRenderer.material.color = _originalColor;
            }

            if (_pointedColor != currentRenderer.material.color)
            {
                _originalColor = currentRenderer.material.color;
            }
            
            currentRenderer.material.color = _pointedColor;
            _lastObjectRenderer = currentRenderer;
        }
        else
        {
            if (_lastObjectRenderer is not null)
            {
                _lastObjectRenderer.material.color = _originalColor;
                _lastObjectRenderer = null;
            }
        }
    }
}


