using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthVisual : MonoBehaviour
{
	[SerializeField]
	private PlayerHealth _playerHealth;

	[SerializeField]
	private Image _healthBar;

	private void OnEnable()
	{
		_playerHealth.OnHealthChangedEvent += UpdateHealthBar;
	}

	private void UpdateHealthBar(float percentage)
	{
		_healthBar.fillAmount = percentage;
	}

	private void OnDisable()
	{
		if (_playerHealth != null)
		{
			_playerHealth.OnHealthChangedEvent -= UpdateHealthBar;
		}
	}
}
