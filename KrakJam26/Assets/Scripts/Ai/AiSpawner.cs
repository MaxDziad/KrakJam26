using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour
{
	private const float MIN_DISTANCE_FROM_PLAYER = 10f;

	private float _spawnerCooldownTimer = 4f;
	private bool _isOnCooldown = false;
	private Coroutine _cooldownCoroutine;

	[SerializeField]
	private List<AiAgentType> _supportedAiAgentTypes = new List<AiAgentType>();

	public bool CanSpawnThisAgent(AiAgentType agentType)
	{
		return !_isOnCooldown && IsFarEnoughFromPlayer() && _supportedAiAgentTypes.Contains(agentType);
	}

	public void SpawnAiAgent(AiAgentBase aiAgentPrefab)
	{
		Instantiate(aiAgentPrefab, transform.position, transform.rotation);

		if (_cooldownCoroutine != null)
		{
			StopCoroutine(_cooldownCoroutine);
			_cooldownCoroutine = null;
		}

		_isOnCooldown = true;
		_cooldownCoroutine = StartCoroutine(Cooldown());
	}

	private IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(_spawnerCooldownTimer);
		_isOnCooldown = false;
		_cooldownCoroutine = null;
	}

	private bool IsFarEnoughFromPlayer()
	{
		return Vector3.Distance(transform.position, PlayerSystemManager.Instance.PlayerController.transform.position) >= MIN_DISTANCE_FROM_PLAYER;
	}

	private void Awake()
	{
		AiAgentSystemManager.Instance.RegisterSpawner(this);
	}

	private void OnDestroy()
	{
		if (_cooldownCoroutine != null)
		{
			StopCoroutine(_cooldownCoroutine);
			_cooldownCoroutine = null;
		}

		AiAgentSystemManager.Instance.UnregisterSpawner(this);
	}
}
