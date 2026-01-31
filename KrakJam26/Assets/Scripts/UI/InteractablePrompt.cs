using TMPro;
using UnityEngine;

public class InteractablePrompt : MonoBehaviour
{
	[SerializeField]
	private Interactor _interactor;

	[SerializeField]
	private GameObject _promptUI;

	[SerializeField]
	private TextMeshProUGUI _promptText;

	private void Start()
	{
		_interactor.OnInteractableDetectedEvent += ShowPrompt;
		_interactor.OnInteractableLostEvent += HidePrompt;
	}

	private void ShowPrompt(IInteractable interactable)
	{
		_promptText.text = interactable.GetPromptText();
		_promptUI.SetActive(true);
	}

	private void HidePrompt(IInteractable interactable)
	{
		_promptUI.SetActive(false);
	}

	private void OnDestroy()
	{
		if (_interactor != null)
		{
			_interactor.OnInteractableDetectedEvent -= ShowPrompt;
			_interactor.OnInteractableLostEvent -= HidePrompt;
		}
	}
}

