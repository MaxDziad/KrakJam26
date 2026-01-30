using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour, InputActions.IGameplayActions
{
	[SerializeField]
	private CinemachineCamera _playerCamera;

	[SerializeField]
	private float _playerSpeed = 5;

	private Vector2 _movementInput = Vector2.zero;
	private InputActions _inputActions;
	private CharacterController _characterController;
	private Vector3 _movementDirection;

	private Transform CameraTransform => _playerCamera.transform;

	public void OnMove(InputAction.CallbackContext context)
	{
		_movementInput = context.ReadValue<Vector2>();
		_movementInput.Normalize();
	}

	public void OnWearSelectedMask(InputAction.CallbackContext context)
	{
		MaskStateManager.Instance.ChangeMask(MaskType.Silent);
	}

	public void OnChangeMask(InputAction.CallbackContext context)
	{
		throw new System.NotImplementedException();
	}

	public void OnShout(InputAction.CallbackContext context)
	{
		throw new System.NotImplementedException();
	}

	public void Start()
	{
		_inputActions = new InputActions();
		_inputActions.Gameplay.SetCallbacks(this);
		_inputActions.Gameplay.Enable();

		_characterController = GetComponent<CharacterController>();
	}

	public void FixedUpdate()
	{
		if (_movementInput != Vector2.zero)
		{
			Vector3 camForward = Vector3.ProjectOnPlane(CameraTransform.forward, Vector3.up).normalized;
			Vector3 camRight = Vector3.ProjectOnPlane(CameraTransform.right, Vector3.up).normalized;
			_movementDirection = camRight * _movementInput.x + camForward * _movementInput.y;

			if (_movementDirection.sqrMagnitude > 1f)
			{
				_movementDirection.Normalize();
			}

			Vector3 movement = _movementDirection * _playerSpeed * Time.fixedDeltaTime;
			_characterController.Move(movement);
		}
	}

	public void OnDestroy()
	{
		_inputActions.Gameplay.Disable();
	}
}
