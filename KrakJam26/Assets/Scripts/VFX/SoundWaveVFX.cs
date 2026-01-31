using UnityEngine;

public class SoundWaveVFX : MonoBehaviour
{
    [SerializeField] private float growRate;
    [SerializeField] private float lifetime;

    private float currentSize = 0;
    private Color currentColor;
    private Color lastSetColor;
    private MeshRenderer ownMeshRenderer;

    public static float GlobalOpacity = 1.0f;
    
    private void Awake()
    {
        ownMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        currentSize += growRate * Time.deltaTime;
        transform.localScale = Vector3.one * currentSize;

        RefreshColor();
    }

    private void RefreshColor()
    {
        var newColor = currentColor;
        newColor.a *= GlobalOpacity;

        if (newColor == lastSetColor)
        {
            return;
        }

        var material = ownMeshRenderer.material;
        material.SetColor("_Color", newColor);
        lastSetColor = newColor;
    }

    public void SetColor(Color color)
    {
        currentColor = color;
    }
}
