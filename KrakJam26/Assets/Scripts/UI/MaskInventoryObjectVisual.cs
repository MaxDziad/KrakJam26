using UnityEngine;
using UnityEngine.UI;

public class MaskInventoryObjectVisual : MonoBehaviour
{
    [SerializeField] private MaskType _maskType;

    [Header("Visual Colors")]
    [SerializeField] private Color neutralStateColor;
    [SerializeField] private Color selectedStateColor;
    [SerializeField] private Color wearingStateColor;
    [SerializeField] private Color lockedStateColor = Color.black; 

    private Image _maskImage;

    public MaskType MaskType => _maskType;
    public Image MaskImage => _maskImage;

    private void Awake()
    {
        _maskImage = GetComponent<Image>();
        SetState(MaskInventoryState.Locked);
    }

    public bool HasImage => _maskImage.sprite != null;

    public void SetMaskImage(Sprite maskSprite)
    {
        _maskImage.sprite = maskSprite;
    }

    public void SetState(MaskInventoryState state)
    {
        _maskImage.color = state switch
        {
            MaskInventoryState.Selected => selectedStateColor,
            MaskInventoryState.Wearing => wearingStateColor,
            MaskInventoryState.Locked  => lockedStateColor,
            _ => neutralStateColor,
        };
    }

    public enum MaskInventoryState
    {
        Locked, 
        Neutral,
        Selected,
        Wearing
    }
}
