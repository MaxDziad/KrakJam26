using UnityEngine;
using UnityEngine.UI;

public class MaskInventoryObject : MonoBehaviour
{
	[SerializeField] MaskType _maskType;

	[Header("Visual Colors")]
	[SerializeField] private Color neutralStateColor;
	[SerializeField] private Color selectedStateColor;
	[SerializeField] private Color usedStateColor;

	private Image _maskImage;
	private Button _button;

	public MaskType MaskType => _maskType;

	private void Awake()
	{
		_maskImage = GetComponent<Image>();
		_button = GetComponent<Button>();
	}

	public bool HasImage => _maskImage.sprite != null;

	public void SetMaskImage(Sprite maskSprite)
	{
		_maskImage.sprite = maskSprite;
	}

	public void SetState(State state)
	{
		_maskImage.color = state switch
		{
			State.Selected => selectedStateColor,
			State.Used => usedStateColor,
			_ => neutralStateColor,
		};
	}

	public enum State
	{
		Neutral,
		Selected,
		Used
	}
}
