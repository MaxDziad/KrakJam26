using System;
using UnityEngine;

public class MaskStateManager : MonoBehaviour
{
	public static MaskStateManager Instance { get; private set; }

	public event Action<MaskType> OnMaskChangeStartedEvent;
	public event Action<MaskType> OnMaskChangedEvent;

	public MaskType CurrentMask { get; private set; } = MaskType.None;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void ChangeMask(MaskType newMask)
	{
		if (newMask == CurrentMask)
		{
			return;
		}

		OnMaskChangeStartedEvent?.Invoke(newMask);
		// Need to animate mask changing for some time, then after that notify that the mask has changed.
		// Swap mask and call OnMaskChangedEvent after animation is done.
	}
}
