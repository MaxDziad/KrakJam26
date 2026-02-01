using UnityEngine;

public class ActivableSound : ActivableBase
{
	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public override void Activate()
	{
		audioSource?.Play();
	}
}
