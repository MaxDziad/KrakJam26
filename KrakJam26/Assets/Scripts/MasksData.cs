using UnityEngine;

[CreateAssetMenu(fileName = "MasksData", menuName = "Scriptable Objects/MasksData")]
public class MasksData : ScriptableObject
{
	[SerializeField]
	private Sprite _deafMaskSprite;

	[SerializeField]
	private Sprite _blindMaskSprite;

	[SerializeField]
	private Sprite _silentMaskSprite;

	public Sprite GetMaskSprite(MaskType type)
	{
		return type switch
		{
			MaskType.Deaf => _deafMaskSprite,
			MaskType.Blind => _blindMaskSprite,
			MaskType.Silent => _silentMaskSprite,
			_ => null,
		};
	}
}
