using UnityEngine;

public class ActivableAnimationTrigger : ActivableBase
{
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private string triggerName = "Activate";

	private int triggerHash;

	private void Awake()
	{
		triggerHash = Animator.StringToHash(triggerName);
	}

	public override void Activate()
	{
		if (animator != null)
		{
			animator.SetTrigger(triggerHash);
		}
	}
}
