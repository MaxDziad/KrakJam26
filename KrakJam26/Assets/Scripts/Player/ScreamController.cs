using UnityEngine;

public class ScreamController : MonoBehaviour
{
    [SerializeField] private GameObject screamProjectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float projectileSpeed = 4f;
    [SerializeField] private float cooldown = 2f;
    private float cooldownTimer = 0f;

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

        private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }


    private void Shoot()
    {
        MaskType currentMask = MaskSystemManager.Instance.CurrentMask;
        if (currentMask == MaskType.Silent || currentMask == MaskType.None)
            return;

        if (cooldownTimer > 0f) return;

        Vector3 direction = playerController.screamDirection;

        GameObject projectile = Instantiate(
            screamProjectilePrefab,
            spawnPoint.position,
            Quaternion.LookRotation(direction)
        );

        projectile.GetComponent<ScreamProjectile>().Initialize(direction, projectileSpeed);

        cooldownTimer = cooldown;
    }
}
