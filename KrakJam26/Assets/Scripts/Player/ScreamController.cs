using UnityEngine;

public class ScreamController : MonoBehaviour
{
    [SerializeField] private GameObject screamProjectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float projectileSpeed = 4f;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.OnShoutEvent += Shoot;
    }

    private void OnDisable()
    {
        playerController.OnShoutEvent -= Shoot;
    }

    private void Shoot()
    {
        Vector3 direction = playerController.screamDirection;

        GameObject projectile = Instantiate(
            screamProjectilePrefab,
            spawnPoint.position,
            Quaternion.LookRotation(direction)
        );

        projectile.GetComponent<ScreamProjectile>().Initialize(direction, projectileSpeed);
    }
}
