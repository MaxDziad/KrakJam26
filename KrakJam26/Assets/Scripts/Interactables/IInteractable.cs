using System;

public interface IInteractable
{
	public event Action OnDisabledEvent;
	void Interact();
	string GetPromptText();
}
