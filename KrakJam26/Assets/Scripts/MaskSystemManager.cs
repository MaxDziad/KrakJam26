using System;
using System.Collections.Generic;
using UnityEngine;

public class MaskSystemManager : MonoBehaviour
{
    public static MaskSystemManager Instance { get; private set; }

    public event Action<MaskType> OnMaskChangeStartedEvent;
    public event Action<MaskType> OnMaskChangedEvent;

    [SerializeField] private MasksData masksData;
    [SerializeField] private HashSet<MaskType> unlockedTypes = new();

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
            return false;

        if (!IsMaskUnlocked(newMask))
        {
            Debug.Log($"mask locked");
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

    public void UnlockMask(MaskType mask)
    {
        if (mask == MaskType.None || IsMaskUnlocked(mask))
            return;

        unlockedTypes.Add(mask);
        TryChangeMask(mask);
    }

    public bool IsMaskUnlocked(MaskType mask)
    {
        return mask == MaskType.None || unlockedTypes.Contains(mask);
    }
}
