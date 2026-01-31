using UnityEngine;

[ExecuteAlways]
public class GlobalSphereMaskController : MonoBehaviour
{
    private static GlobalSphereMaskController _instance;

    private void OnEnable()
    {
        if (_instance != null)
        {
            Debug.LogError("There can only be one GlobalSphereMaskController in the scene!");
            return;
        }
        _instance = this;
    }

    private void OnDisable()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Update()
    {
        if (_instance != this)
        {
            return;
        }

        Shader.SetGlobalVector("_GlobalSphereMaskOrigin", transform.position);
        Shader.SetGlobalFloat("_GlobalSphereMaskRadius", transform.lossyScale.x * 0.5f);
    }
}
