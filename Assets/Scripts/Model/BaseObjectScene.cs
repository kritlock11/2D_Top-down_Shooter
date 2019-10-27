using UnityEngine;

public abstract class BaseObjectScene : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D Rigidbody { get; private set; }
    [HideInInspector] public Transform Transform { get; private set; }
    public string Name
    {
        get => gameObject.name;
        set => gameObject.name = value;
    }
    private bool _isVisible;
    private Color _color;
    private int _layer;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            var tempRenderer = GetComponent<SpriteRenderer>();
            if (tempRenderer)
                tempRenderer.enabled = _isVisible;
            if (transform.childCount <= 0) return;
            foreach (Transform d in transform)
            {
                tempRenderer = d.gameObject.GetComponent<SpriteRenderer>();
                if (tempRenderer)
                    tempRenderer.enabled = _isVisible;
            }
        }
    }
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            AskColor(transform, _color);
        }
    }
    public int Layer
    {
        get => _layer;
        set
        {
            _layer = value;
            AskLayer(Transform, _layer);
        }
    }
    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Transform = transform;
    }
    private void AskColor(Transform obj, Color color)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            foreach (var curMaterial in renderer.materials)
            {
                curMaterial.color = color;
            }
        }

        if (obj.childCount <= 0) return;
        foreach (Transform d in obj)
        {
            AskColor(d, color);
        }
    }
    private void AskLayer(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        if (obj.childCount <= 0) return;

        foreach (Transform child in obj)
        {
            AskLayer(child, layer);
        }
    }
}
