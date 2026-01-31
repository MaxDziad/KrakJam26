using UnityEngine;

public class DamagingCollider : MonoBehaviour
{
	[SerializeField]
	private int _damage = 10;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PlayerHealth>(out var playerHealth))
		{
			playerHealth.TakeDamage(_damage);
		}
	}
}
