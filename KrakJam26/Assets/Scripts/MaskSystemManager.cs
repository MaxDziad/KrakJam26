using System;
using UnityEngine;

public class MaskSystemManager : MonoBehaviour
{
	public static MaskSystemManager Instance { get; private set; }

	public event Action<MaskType> OnMaskChangeStartedEvent;
	public event Action<MaskType> OnMaskChangedEvent;

	[SerializeField]
	private MasksData masksData;

	private MaskVisuals maskVisuals;

	public MasksData MasksData => masksData;
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
		maskVisuals = GetComponent<MaskVisuals>();
	}

	public bool TryChangeMask(MaskType newMask)
	{
		if (newMask == CurrentMask || maskVisuals.IsChangeInProgress)
		{
			return false;
		}

		OnMaskChangeStartedEvent?.Invoke(newMask);
		
		maskVisuals.StartVisualChange(newMask, () =>
		{
			CurrentMask = newMask;
			OnMaskChangedEvent?.Invoke(newMask);
		});

		return true;
	}
}
