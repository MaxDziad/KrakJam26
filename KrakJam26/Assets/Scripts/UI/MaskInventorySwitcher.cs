using System;
using UnityEngine;
using UnityEngine.UI;

public class MaskInventorySwitcher : MonoBehaviour
{
	[SerializeField]
	private PlayerController _playerController;

	[SerializeField]
	private MaskInventoryObject[] _maskInventoryObjects;

	[SerializeField]
	private Image _maskPortraitImage;

	private int _activeMaskIndex = -1;
	private int _selectedMaskIndex = 0;

	private void OnEnable()
	{
		_playerController.OnSelectNextMaskEvent += OnSelectNextMask;
		_playerController.OnWearSelectedMaskEvent += OnWearSelectedMask;
		_playerController.OnSelectSpecificMaskEvent += OnWearSpecificMask;
	}

	private void Start()
	{
		MaskSystemManager.Instance.OnMaskChangedEvent += OnMaskChanged;
		MaskSystemManager.Instance.OnMaskChangeStartedEvent += OnMaskChangeStarted;
		RefreshButtons();
	}

	private void OnMaskChangeStarted(MaskType type)
	{
		_maskPortraitImage.gameObject.SetActive(false);
		_maskPortraitImage.sprite = MaskSystemManager.Instance.MasksData.GetMaskSprite(type);
	}

	private void OnMaskChanged(MaskType type)
	{
		_maskPortraitImage.gameObject.SetActive(true);
	}

	private void OnSelectNextMask()
	{
		for(int i = 0; i < 2; i++)
		{
			_selectedMaskIndex = (_selectedMaskIndex + 1) % _maskInventoryObjects.Length;

			if (_selectedMaskIndex != _activeMaskIndex)
			{
				break;
			}
		}
		RefreshButtons();
	}

	private void OnWearSelectedMask()
	{
		var targetType = _maskInventoryObjects[_selectedMaskIndex].MaskType;

		if (!MaskSystemManager.Instance.TryChangeMask(targetType))
		{
			return;
		}

		_activeMaskIndex = _selectedMaskIndex;
		RefreshButtons();
	}

	private void OnWearSpecificMask(int index)
	{
		_selectedMaskIndex = index % _maskInventoryObjects.Length;
		OnWearSelectedMask();
		RefreshButtons();
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
		_maskInventoryObjects[index].SetState(
			index == _activeMaskIndex ? MaskInventoryObject.MaskInventoryState.Wearing :
			index == _selectedMaskIndex ? MaskInventoryObject.MaskInventoryState.Selected : 
			MaskInventoryObject.MaskInventoryState.Neutral
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
