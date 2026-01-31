using System;
using UnityEngine;

public class MaskStateManager : MonoBehaviour
{
	public static MaskStateManager Instance { get; private set; }

	public event Action<MaskType> OnMaskChangeStartedEvent;
	public event Action<MaskType> OnMaskChangedEvent;

	public MaskType CurrentMask { get; private set; } = MaskType.None;

	private MaskVisuals maskVisuals;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		maskVisuals = GetComponent<MaskVisuals>();
	}

	public void ChangeMask(MaskType newMask)
	{
		if (newMask == CurrentMask || maskVisuals.IsChangeInProgress)
		{
			return;
		}

		OnMaskChangeStartedEvent?.Invoke(newMask);
		
		maskVisuals.StartVisualChange(newMask, () =>
		{
			CurrentMask = newMask;
			OnMaskChangedEvent?.Invoke(newMask);
		});
	}
}
