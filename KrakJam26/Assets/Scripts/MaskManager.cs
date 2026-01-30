using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MaskStateManager : MonoBehaviour
{
    public static MaskStateManager Instance { get; private set; }

    public bool MaskSilent { get; private set; } = true;
    public bool MaskDeaf { get; private set; } = true;
    public bool MaskBlind { get; private set; } = true;

    [SerializeField] private InputActionReference maskSilentAction;
    [SerializeField] private InputActionReference maskDeafAction;
    [SerializeField] private InputActionReference maskBlindAction;

    public UnityEvent OnMaskSilentActivated;
    public UnityEvent OnMaskDeafActivated;
    public UnityEvent OnMaskBlindActivated;

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
            maskSilentAction.action.performed += OnMaskSilentInput;

        if (maskDeafAction != null)
            maskDeafAction.action.performed += OnMaskDeafInput;

        if (maskBlindAction != null)
            maskBlindAction.action.performed += OnMaskBlindInput;
    }

    private void OnDisable()
    {
        if (maskSilentAction != null)
            maskSilentAction.action.performed -= OnMaskSilentInput;

        if (maskDeafAction != null)
            maskDeafAction.action.performed -= OnMaskDeafInput;

        if (maskBlindAction != null)
            maskBlindAction.action.performed -= OnMaskBlindInput;
    }

    private void OnMaskSilentInput(InputAction.CallbackContext ctx)
        => ActivateMaskSilent();

    private void OnMaskDeafInput(InputAction.CallbackContext ctx)
        => ActivateMaskDeaf();

    private void OnMaskBlindInput(InputAction.CallbackContext ctx)
        => ActivateMaskBlind();

    private void ActivateMaskSilent()
    {
        SetMasks(false, true, true);
        OnMaskSilentActivated?.Invoke();
    }

    private void ActivateMaskDeaf()
    {
        SetMasks(true, false, true);
        OnMaskDeafActivated?.Invoke();
    }

    private void ActivateMaskBlind()
    {
        SetMasks(true, true, false);
        OnMaskBlindActivated?.Invoke();
    }

    private void SetMasks(bool silent, bool deaf, bool blind)
    {
        MaskSilent = silent;
        MaskDeaf = deaf;
        MaskBlind = blind;

        Debug.Log($"Masks => Silent:{MaskSilent} Deaf:{MaskDeaf} Blind:{MaskBlind}");
    }
}
