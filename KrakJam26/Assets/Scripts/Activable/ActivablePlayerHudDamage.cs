using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ActivablePlayerHudDamage : ActivableBase
{
	[SerializeField]
	private Image _image;

	[SerializeField]
	private Color _damagedColor = Color.red;

	[SerializeField]
	private float _flashInDuration = 0.05f; // Very fast

	[SerializeField]
	private float _fadeOutDuration = 1.5f; // Much slower

	private Coroutine _fadeRoutine;
	private Color _originalColor = Color.white;

	public override void Activate()
	{
		// If we get hit again while fading, stop the old fade and start fresh
		if (_fadeRoutine != null)
		{
			StopCoroutine(_fadeRoutine);
		}

		_fadeRoutine = StartCoroutine(DamageFlashRoutine());
	}

	private IEnumerator DamageFlashRoutine()
	{
		float elapsed = 0f;

		// 1. Fast Fade to Damaged Color
		while (elapsed < _flashInDuration)
		{
			elapsed += Time.deltaTime;
			_image.color = Color.Lerp(_image.color, _damagedColor, elapsed / _flashInDuration);
			yield return null;
		}

		// Ensure we hit the exact color
		_image.color = _damagedColor;
		elapsed = 0f;

		// 2. Slow Fade back to White (or original color)
		while (elapsed < _fadeOutDuration)
		{
			elapsed += Time.deltaTime;
			_image.color = Color.Lerp(_damagedColor, _originalColor, elapsed / _fadeOutDuration);
			yield return null;
		}

		_image.color = _originalColor;
		_fadeRoutine = null;
	}
}
