using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneInteractable : MonoBehaviour, IInteractable
{
    public event Action OnDisabledEvent;

    [SerializeField]
    private string sceneName = "";

    [SerializeField]
    private string interactionPrompt = "Jump in?";

    [SerializeField] 
    private TransitionFade transitionFade;

	public string GetPromptText()
    {
        return interactionPrompt;
    }

    public void Interact()
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogWarning($"{nameof(ChangeSceneInteractable)} on '{gameObject.name}' has no sceneName set.");
            return;
        }
        TransitionFade.Transition(ChangeLevel);
    }

    private void ChangeLevel()
    {
        // Load the specified scene by name. Switch to LoadSceneAsync if non-blocking load is desired.
        SceneManager.LoadScene(sceneName);
    }

    private void OnDisable()
    {
        OnDisabledEvent?.Invoke();
    }
}
