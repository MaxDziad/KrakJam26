using UnityEngine;

public class MaskInventorySwitcher : MonoBehaviour
{
	[SerializeField]
	private PlayerController _playerController;

	[SerializeField]
	private MaskInventoryObject[] _maskInventoryObjects;

	private int _activeMaskIndex = 0;
	private int _selectedMaskIndex = 0;

	private void OnEnable()
	{
		_playerController.OnChangeMaskEvent += OnChangeMask;
		_playerController.OnWearSelectedMaskEvent += OnWearSelectedMask;
	}

	private void Start()
	{
		OnWearSelectedMask();
	}

	private void OnChangeMask()
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
		_activeMaskIndex = _selectedMaskIndex;
		MaskStateManager.Instance.ChangeMask(_maskInventoryObjects[_activeMaskIndex].MaskType);
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
			index == _activeMaskIndex ? MaskInventoryObject.State.Used :
			index == _selectedMaskIndex ? MaskInventoryObject.State.Selected : 
			MaskInventoryObject.State.Neutral
		);
	}

	private void OnDisable()
	{
		if (_playerController != null)
		{
			_playerController.OnChangeMaskEvent -= OnChangeMask;
			_playerController.OnWearSelectedMaskEvent -= OnWearSelectedMask;
		}
	}
}
