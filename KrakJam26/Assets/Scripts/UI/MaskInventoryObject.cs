using UnityEngine;
using UnityEngine.UI;

public class MaskInventoryObject : MonoBehaviour
{
	[SerializeField] MaskType _maskType;

	[Header("Visual Colors")]
	[SerializeField] private Color neutralStateColor;
	[SerializeField] private Color selectedStateColor;
	[SerializeField] private Color wearingStateColor;

	private Image _maskImage;

	public MaskType MaskType => _maskType;
	public Image MaskImage => _maskImage;

	private void Awake()
	{
		_maskImage = GetComponent<Image>();
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
			_ => neutralStateColor,
		};
	}

	public enum MaskInventoryState
	{
		Neutral,
		Selected,
		Wearing
	}
}
