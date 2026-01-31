using Unity.Behavior;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
	private const string VariableName = "IsPlayerVisible";

	[SerializeField]
	private BehaviorGraphAgent _agent;

	[SerializeField]
	private LayerMask _playerLayer;

	[SerializeField]
	private Transform _rayOrigin;

	private Transform _rayTarget;

	private bool _isPlayerVisible = false;

	private void Start()
	{
		_rayTarget = PlayerSystemManager.Instance.PlayerController.transform;
	}

	public void UpdatePlayerVisibility()
	{
		if (_rayOrigin == null || _rayTarget == null)
		{
			_isPlayerVisible = false;
			return;
		}

		if (Physics.Linecast(_rayOrigin.position, _rayTarget.position, out RaycastHit hit))
		{
			Transform hitTransform = hit.collider.transform;
			bool hitIsPlayerTransform = hitTransform == _rayTarget || hitTransform.IsChildOf(_rayTarget);
			bool hitIsPlayerLayer = (_playerLayer.value & (1 << hit.collider.gameObject.layer)) != 0;
			_isPlayerVisible = hitIsPlayerTransform || hitIsPlayerLayer;
		}
		else
		{
			_isPlayerVisible = false;
		}
	}

	private void Update()
	{
		UpdatePlayerVisibility();
		_agent.SetVariableValue(VariableName, _isPlayerVisible);
	}
}
