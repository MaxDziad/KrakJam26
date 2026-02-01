using UnityEngine;

public class ScreamProjectile : MonoBehaviour
{
	[SerializeField]
	private float lifetime = 5f;

	private Vector3 direction;
	private float speed;

	public void Initialize(Vector3 dir, float spd)
	{
		direction = dir;
		speed = spd;

		Destroy(gameObject, lifetime);
	}

	private void FixedUpdate()
	{
		transform.position += direction * speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<ScreamDamageReceiver>(out var damageReceiver))
		{
			damageReceiver.ReceiveDamage();
			Destroy(gameObject);
		}
	}
}
