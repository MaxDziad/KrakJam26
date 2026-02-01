using UnityEngine;

public class PlayerDamagingCollider : MonoBehaviour
{
	[SerializeField]
	private int _damage = 10;

	[SerializeField]
	private ActivableBase _activableOnHit;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PlayerHealth>(out var playerHealth))
		{
			playerHealth.TakeDamage(_damage);
			_activableOnHit?.Activate();
		}
	}
}
