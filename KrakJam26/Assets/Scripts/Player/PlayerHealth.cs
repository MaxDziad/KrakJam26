using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public event Action<float> OnHealthChangedEvent;
	public event Action OnDeathEvent;

	[SerializeField]
	private int _maxHealth = 100;

	[SerializeField]
	private ActivableBase _onDeathActivable;

	[SerializeField]
	private ActivableBase _onDamageActivable;

	private float _healthPercentage;
	private int _currentHealth;

	public float HealthPercentage => _healthPercentage;
	public int CurrentHealth
	{
		get => _currentHealth;
		private set
		{
			UpdateHealth(value);
		}
	}

	private void UpdateHealth(int value)
	{
		_currentHealth = Mathf.Clamp(value, 0, _maxHealth);
		_healthPercentage = (float)_currentHealth / _maxHealth;
		OnHealthChangedEvent?.Invoke(_healthPercentage);
		_onDamageActivable?.Activate();

		if (_currentHealth <= 0)
		{
			OnDeathEvent?.Invoke();
			_onDeathActivable?.Activate();
		}
	}

	private void Awake()
	{
		CurrentHealth = _maxHealth;
	}

	public void TakeDamage(int damageAmount)
	{
		CurrentHealth -= damageAmount;
	}
}
