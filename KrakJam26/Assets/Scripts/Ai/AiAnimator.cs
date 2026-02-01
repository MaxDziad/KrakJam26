using UnityEngine;

public class AiAnimator : MonoBehaviour
{
    [SerializeField]
    private AiAgentBase _aiAgent;

	private Animator _animator;
    public Animator Animator => _animator ??= GetComponent<Animator>();

	private void FixedUpdate()
	{
		if (_aiAgent != null)
		{
			_aiAgent.NavmeshAgent.updateRotation = false;
			Vector3 localVelocity = _aiAgent.NavmeshAgent.velocity;
			Animator.SetFloat("ForwardSpeed", localVelocity.z);
			Animator.SetFloat("RightSpeed", localVelocity.x);
		}
	}
}
