using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskStateManager : MonoBehaviour
{
    public static MaskStateManager Instance { get; private set; }

    public enum MaskType
    {
        SILENT,
        DEAF,
        BLIND
    }

    public MaskType CurrentMask { get; private set; } = MaskType.SILENT;

    [SerializeField] private InputActionReference maskSilentAction;
    [SerializeField] private InputActionReference maskDeafAction;
    [SerializeField] private InputActionReference maskBlindAction;

    public event Action<MaskType> OnMaskChanged;

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

    private void OnEnable()
    {
        if (maskSilentAction != null)
            maskSilentAction.action.performed += ctx => ActivateMask(MaskType.SILENT);

        if (maskDeafAction != null)
            maskDeafAction.action.performed += ctx => ActivateMask(MaskType.DEAF);

        if (maskBlindAction != null)
            maskBlindAction.action.performed += ctx => ActivateMask(MaskType.BLIND);
    }

    private void OnDisable()
    {
        if (maskSilentAction != null)
            maskSilentAction.action.performed -= ctx => ActivateMask(MaskType.SILENT);

        if (maskDeafAction != null)
            maskDeafAction.action.performed -= ctx => ActivateMask(MaskType.DEAF);

        if (maskBlindAction != null)
            maskBlindAction.action.performed -= ctx => ActivateMask(MaskType.BLIND);
    }

    private void ActivateMask(MaskType mask)
    {
        if (CurrentMask == mask) return;

        CurrentMask = mask;
        Debug.Log($"Mask changed to: {CurrentMask}");

        OnMaskChanged?.Invoke(CurrentMask);
    }
    
    public bool IsSilent() => CurrentMask == MaskType.SILENT;
    public bool IsDeaf() => CurrentMask == MaskType.DEAF;
    public bool IsBlind() => CurrentMask == MaskType.BLIND;
}
