using UnityEngine;

public class SoundWaveVFX : MonoBehaviour
{

    [SerializeField] private float growRate;
    [SerializeField] private float lifetime;

    private float currentSize = 0;

    private MeshRenderer ownMeshRenderer;
    
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        currentSize += growRate * Time.deltaTime;
        transform.localScale = Vector3.one * currentSize;
    }


    public void SetColor(Color color)
    {
        if (ownMeshRenderer == null && !TryGetComponent<MeshRenderer>(out ownMeshRenderer))
        {
            return;    
        }
        
        var material = ownMeshRenderer.material;
        material.SetColor("_Color", color);
    }
}
