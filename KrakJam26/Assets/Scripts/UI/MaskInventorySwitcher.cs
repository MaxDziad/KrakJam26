using System;
using System.Collections.Generic;
using UnityEngine;

public class MaskInventorySwitcher : MonoBehaviour
{
	[SerializeField]
	private PlayerController _playerController;

	[SerializeField]
	private MaskInventoryObject _mask1;

	[SerializeField]
	private MaskInventoryObject _mask2;

	private MaskInventoryObject _currentSelectedMaskInventoryObject;

	private void OnEnable()
	{
		_playerController.OnChangeMaskEvent += OnChangeMask;
		_playerController.OnWearSelectedMaskEvent += OnWearSelectedMask;
	}

	private void Start()
	{
		_currentSelectedMaskInventoryObject = _mask1;
		_currentSelectedMaskInventoryObject.SetButtonEnabled(true);
	}

	private void OnChangeMask()
	{
		_currentSelectedMaskInventoryObject.SetButtonEnabled(false);
		_currentSelectedMaskInventoryObject = _currentSelectedMaskInventoryObject == _mask1 ? _mask2 : _mask1;
		_currentSelectedMaskInventoryObject.SetButtonEnabled(true);
	}

	private void OnWearSelectedMask()
	{
		// Handle wear selected mask
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
