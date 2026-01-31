using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class GlobalGeometryMaskController : MonoBehaviour
{
    [SerializeField] private Vector3 positiveAxisBallFlattening = Vector3.one;

    private static GlobalGeometryMaskController _instance;

    private void OnEnable()
    {
        if (_instance != null)
        {
            Debug.LogError("There can only be one GlobalGeometryMaskController in the scene!");
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

        Shader.SetGlobalMatrix("_GlobalGeometryMaskMatrix", transform.worldToLocalMatrix);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var prevMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        int segments = 12;
        float height = 1f;
        float radius = 0.5f;

        Vector3 apex = Vector3.zero;
        float twoPi = Mathf.PI * 2f;

        Vector3 prev = new Vector3(Mathf.Cos(0f) * radius, Mathf.Sin(0f) * radius, height);
        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            float ang = t * twoPi;
            Vector3 curr = new Vector3(Mathf.Cos(ang) * radius, Mathf.Sin(ang) * radius, height);

            // base circle segment
            Gizmos.DrawLine(prev, curr);
            // side to apex (origin)
            Gizmos.DrawLine(curr, apex);

            prev = curr;
        }
        Gizmos.matrix = prevMatrix;
    }
}
