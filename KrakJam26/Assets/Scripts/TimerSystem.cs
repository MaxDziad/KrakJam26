using System;
using System.Collections;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
	public static TimerSystem Instance { get; private set; }

	public event Action OnTimerStart;
	public event Action<float> OnTimerUpdate;
	public event Action OnTimerEnd;

	private float _currentTimeLeft;

	public float CurrentTimeLeft => _currentTimeLeft;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	public void StartTimer(float duration)
	{
		_currentTimeLeft = duration;
		OnTimerStart?.Invoke();
		StartCoroutine(TimerCoroutine());
	}

	IEnumerator TimerCoroutine()
	{
		while (_currentTimeLeft >= 0)
		{
			_currentTimeLeft -= Time.deltaTime;
			OnTimerUpdate?.Invoke(_currentTimeLeft);
			yield return null;
		}
		
		OnTimerEnd?.Invoke();
	}
}
