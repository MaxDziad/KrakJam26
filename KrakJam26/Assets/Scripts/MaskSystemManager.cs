using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaskSystemManager : MonoBehaviour
{
    public static MaskSystemManager Instance { get; private set; }

    public event Action<MaskType> OnMaskChangeStartedEvent;
    public event Action<MaskType> OnMaskChangedEvent;

    [SerializeField] private MasksData masksData;
    [SerializeField] private List<MaskType> unlockedTypes = new();
    [SerializeField] private MaskType startingMask = MaskType.Silent;

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

        TryChangeMask(startingMask, true);
    }

    public bool TryChangeMask(MaskType newMask, bool force = false)
    {
        if (!force)
        {
            if (newMask == CurrentMask || maskVisuals.IsChangeInProgress)
            {
                return false;
            }

            if (!IsMaskUnlocked(newMask))
            {
                Debug.Log($"mask locked");
                return false;
            }
        }
        
        OnMaskChangeStartedEvent?.Invoke(newMask);

        maskVisuals.StartVisualChange(newMask, force, () =>
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
