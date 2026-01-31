using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System;

public class PlayerController : MonoBehaviour, InputActions.IGameplayActions
{
	public event Action OnWearSelectedMaskEvent;
	public event Action OnChangeMaskEvent;
	public event Action OnShoutEvent;
	public event Action OnSelectNextMaskEvent;
	public event Action<int> OnSelectSpecificMaskEvent;

	[SerializeField]
	private CinemachineCamera _playerCamera;

	[SerializeField]
	private float _playerSpeed = 5;

	[SerializeField]
	private Animator playerAnimator;

	public Vector3 screamDirection { get; private set; }

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
		if (context.started)
		{
			OnWearSelectedMaskEvent?.Invoke();
		}
	}

	public void OnChangeMask(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			OnSelectNextMaskEvent?.Invoke();
		}
	}

	public void OnShout(InputAction.CallbackContext context)
	{
		if (context.performed)
			OnShoutEvent?.Invoke();
	}

	public void OnWearMask1(InputAction.CallbackContext context)
    {
        if (context.started)
		{
			OnSelectSpecificMaskEvent?.Invoke(0);
		}
    }

    public void OnWearMask2(InputAction.CallbackContext context)
    {
        if (context.started)
		{
			OnSelectSpecificMaskEvent?.Invoke(1);
		}
    }

    public void OnWearMask3(InputAction.CallbackContext context)
    {
        if (context.started)
		{
			OnSelectSpecificMaskEvent?.Invoke(2);
		}
    }

	private void Awake()
	{
		_inputActions = new InputActions();
		_inputActions.Gameplay.SetCallbacks(this);
		_inputActions.Gameplay.Enable();

		_characterController = GetComponent<CharacterController>();

		PlayerSystemManager.Instance.RegisterSelf(this);
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

			playerAnimator.SetBool("isMoving", true);
			playerAnimator.SetFloat("MoveX", _movementInput.x);
			playerAnimator.SetFloat("MoveY", _movementInput.y);
		}
		else
		{
			playerAnimator.SetBool("isMoving", false);
			playerAnimator.SetFloat("MoveX", 0f);
			playerAnimator.SetFloat("MoveY", 0f);
		}

		if (_movementDirection.sqrMagnitude > 0.001f)
		{
			Vector3 dir = _movementDirection.normalized;

			dir.x = Mathf.Round(dir.x);
			dir.z = Mathf.Round(dir.z);

			screamDirection = new Vector3(dir.x, 0f, dir.z).normalized;
		}

	}

	public void OnDestroy()
	{
		_inputActions.Gameplay.Disable();
	}
}
