using UnityEngine;

public class SoundWaveVFX : MonoBehaviour
{
    [SerializeField] private float startSize;
    [SerializeField] private float growRate;
    [SerializeField] private float lifetime;
    [SerializeField] private Color color;

    private float currentSize = 0;
    private MeshRenderer ownMeshRenderer;
    private float aliveTime = 0.0f;


    public static float GlobalOpacity = 1.0f;

    
    private void Awake()
    {
        ownMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
        currentSize = startSize;
    }

    private void Update()
    {
        aliveTime += Time.deltaTime;
        currentSize += growRate * Time.deltaTime;
        transform.localScale = Vector3.one * currentSize;

        RefreshColor();
    }

    private void RefreshColor()
    {
        var newColor = color;
        newColor.a *= GlobalOpacity * (1.0f - (aliveTime / lifetime));

        var material = ownMeshRenderer.material;
        material.SetColor("_Color", newColor);
    }
}
