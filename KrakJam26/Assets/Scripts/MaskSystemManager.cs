using System;
using UnityEngine;

public class MaskSystemManager : MonoBehaviour
{
    public static MaskSystemManager Instance { get; private set; }

    public event Action<MaskType> OnMaskChangeStartedEvent;
    public event Action<MaskType> OnMaskChangedEvent;

    [SerializeField] private MasksData masksData;
    [SerializeField] private MaskInventoryObject[] maskInventoryObjects;

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

        MaskInventoryObject maskObj = GetMaskObject(newMask);
        if (maskObj == null || !maskObj.IsUnlocked)
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
        MaskInventoryObject maskObj = GetMaskObject(mask);
        if (maskObj != null && !maskObj.IsUnlocked)
        {
            maskObj.Unlock();
            TryChangeMask(mask);
		}
    }

    public bool IsMaskUnlocked(MaskType mask)
    {
        MaskInventoryObject maskObj = GetMaskObject(mask);
        return maskObj != null && maskObj.IsUnlocked;
    }

    private MaskInventoryObject GetMaskObject(MaskType mask)
    {
        foreach (var obj in maskInventoryObjects)
        {
            if (obj.MaskType == mask)
                return obj;
        }
        return null;
    }
}
