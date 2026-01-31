using System;
using UnityEngine;

public class TimerInteractable : MonoBehaviour, IInteractable
{
	[SerializeField]
	private string _promptText = "Call the machine";

	[SerializeField]
	private ActivableBase _activableOnStart;

	[SerializeField]
	private ActivableBase _activableOnEnd;

	[SerializeField]
	private float _timerDuration = 60f;

	public event Action OnDisabledEvent;

	public string GetPromptText()
	{
		return _promptText;
	}

	public void Interact()
	{
		TimerSystem.Instance.OnTimerEnd += OnTimerEnd;
		TimerSystem.Instance.StartTimer(_timerDuration);
		_activableOnStart?.Activate();
	}

	private void OnTimerEnd()
	{
		TimerSystem.Instance.OnTimerEnd -= OnTimerEnd;
		_activableOnEnd?.Activate();
	}

	private void OnDisable()
	{
		OnDisabledEvent?.Invoke();
	}
}
