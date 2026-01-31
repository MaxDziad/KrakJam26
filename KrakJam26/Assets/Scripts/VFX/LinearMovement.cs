using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    public Vector3 velocity;

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}