using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class MaskInventorySwitcher : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private MaskInventoryObjectVisual[] _maskInventoryObjects;

    [SerializeField]
    private Image _maskPortraitImage;

    [SerializeField]
    private TweenSettings<float> _maskPortraitImageDissolveSettings;

    private int _activeMaskIndex = -1;
    private int _selectedMaskIndex = 0;
    private Material _originalMaskPortraitMaterial;
    private Tween _maskPortraitDissolveTween;

    private void OnEnable()
    {
        _playerController.OnSelectNextMaskEvent += OnSelectNextMask;
        _playerController.OnWearSelectedMaskEvent += OnWearSelectedMask;
        _playerController.OnSelectSpecificMaskEvent += OnWearSpecificMask;

        _originalMaskPortraitMaterial = _maskPortraitImage.material;
        _maskPortraitImage.material = new Material(_originalMaskPortraitMaterial);
    }

    private void Start()
    {
        MaskSystemManager.Instance.OnMaskChangedEvent += OnMaskChanged;
        MaskSystemManager.Instance.OnMaskChangeStartedEvent += OnMaskChangeStarted;
        RefreshButtons();
    }

    private void OnMaskChangeStarted(MaskType type)
    {
        if (_maskPortraitDissolveTween.isAlive)
            _maskPortraitDissolveTween.Stop();

        _maskPortraitDissolveTween = Tween.MaterialProperty(
            _maskPortraitImage.material,
            Shader.PropertyToID("_Value"),
            _maskPortraitImageDissolveSettings.WithDirection(true)
        ).OnComplete(() => OnMaskPortraitFadedOut(type));
    }

    private void OnMaskPortraitFadedOut(MaskType type)
    {
        _maskPortraitImage.sprite = MaskSystemManager.Instance.MasksData.GetMaskSprite(type);
        _maskPortraitDissolveTween = Tween.MaterialProperty(
            _maskPortraitImage.material,
            Shader.PropertyToID("_Value"),
            _maskPortraitImageDissolveSettings.WithDirection(false)
        );
    }

    private void OnMaskChanged(MaskType type)
    {
        _maskPortraitImage.gameObject.SetActive(true);
    }

    private void OnSelectNextMask()
    {
        int startIndex = _selectedMaskIndex;
        int count = _maskInventoryObjects.Length;

        do
        {
            _selectedMaskIndex = (_selectedMaskIndex + 1) % count;

            if (_maskInventoryObjects[_selectedMaskIndex].IsUnlocked &&
                _selectedMaskIndex != _activeMaskIndex)
            {
                RefreshButtons();
                return;
            }

        } while (_selectedMaskIndex != startIndex);

    }

    private void OnWearSelectedMask()
    {
        var maskObj = _maskInventoryObjects[_selectedMaskIndex];

        if (!maskObj.IsUnlocked)
            return;

        var targetType = maskObj.MaskType;

        if (!MaskSystemManager.Instance.TryChangeMask(targetType))
            return;

        _activeMaskIndex = _selectedMaskIndex;
        RefreshButtons();
    }

    private void OnWearSpecificMask(int index)
    {
        index %= _maskInventoryObjects.Length;

        if (!_maskInventoryObjects[index].IsUnlocked)
            return;

        _selectedMaskIndex = index;
        OnWearSelectedMask();
    }

    private void RefreshButtons()
    {
        for (int i = 0; i < _maskInventoryObjects.Length; i++)
        {
            RefreshButtonState(i);
        }
    }

    private void RefreshButtonState(int index)
    {
        var obj = _maskInventoryObjects[index];

        if (!obj.IsUnlocked)
        {
            obj.SetState(MaskInventoryObjectVisual.MaskInventoryState.Locked);
            return;
        }

        obj.SetState(
            index == _activeMaskIndex ? MaskInventoryObjectVisual.MaskInventoryState.Wearing :
            index == _selectedMaskIndex ? MaskInventoryObjectVisual.MaskInventoryState.Selected :
            MaskInventoryObjectVisual.MaskInventoryState.Neutral
        );
    }

    private void OnDisable()
    {
        if (_playerController != null)
        {
            _playerController.OnSelectNextMaskEvent -= OnSelectNextMask;
            _playerController.OnWearSelectedMaskEvent -= OnWearSelectedMask;
            _playerController.OnSelectSpecificMaskEvent -= OnWearSpecificMask;
        }

        Destroy(_maskPortraitImage.material);
        _maskPortraitImage.material = _originalMaskPortraitMaterial;
        _originalMaskPortraitMaterial = null;
    }

    private void OnDestroy()
    {
        if (MaskSystemManager.Instance != null)
        {
            MaskSystemManager.Instance.OnMaskChangedEvent -= OnMaskChanged;
            MaskSystemManager.Instance.OnMaskChangeStartedEvent -= OnMaskChangeStarted;
        }
    }
}
