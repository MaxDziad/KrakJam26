using UnityEngine;

public class TimerVisual : MonoBehaviour
{
	[SerializeField]
	private GameObject _timerPanel;

	[SerializeField]
	private TMPro.TextMeshProUGUI _timerText;

	private TimerSystem TimerSystem => TimerSystem.Instance;

	private void Start()
	{
		if (TimerSystem != null)
		{
			TimerSystem.OnTimerStart += HandleTimerStart;
			TimerSystem.OnTimerUpdate += HandleTimerUpdate;
			TimerSystem.OnTimerEnd += HandleTimerEnd;
		}

		if (_timerPanel != null)
		{
			_timerPanel.SetActive(false);
		}
	}

	private void HandleTimerStart()
	{
		if (_timerText != null)
		{
			_timerText.text = FormatTime(TimerSystem != null ? TimerSystem.CurrentTimeLeft : 0f);
		}

		if (_timerPanel != null)
		{
			_timerPanel.SetActive(true);
		}
	}

	private void HandleTimerUpdate(float timeLeft)
	{
		if (_timerText != null)
		{
			_timerText.text = FormatTime(timeLeft);
		}
	}

	private void HandleTimerEnd()
	{
		if (_timerText != null)
		{
			_timerText.text = FormatTime(0f);
		}

		if (_timerPanel != null)
		{
			_timerPanel.SetActive(false);
		}
	}

	private string FormatTime(float seconds)
	{
		// Ensure non-negative whole seconds
		int totalSeconds = Mathf.Max(0, Mathf.FloorToInt(seconds));
		int minutes = totalSeconds / 60;
		int secs = totalSeconds % 60;
		return string.Format("{0:D2}:{1:D2}", minutes, secs);
	}

	private void OnDestroy()
	{
		if (TimerSystem != null)
		{
			TimerSystem.OnTimerStart -= HandleTimerStart;
			TimerSystem.OnTimerUpdate -= HandleTimerUpdate;
			TimerSystem.OnTimerEnd -= HandleTimerEnd;
		}
	}
}
