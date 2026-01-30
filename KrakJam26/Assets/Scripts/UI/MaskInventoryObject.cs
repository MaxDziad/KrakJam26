using UnityEngine;
using UnityEngine.UI;

public class MaskInventoryObject : MonoBehaviour
{
	private Image _maskImage;
	private Button _button;

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

	public void SetButtonEnabled(bool isEnabled)
	{
		_button.interactable = isEnabled;
	}
}
