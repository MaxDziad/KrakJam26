using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputActions.IGameplayActions
{
	[SerializeField]
	private float _playerSpeed = 5;

	private Vector2 _movementInput = Vector2.zero;
	private InputActions _inputActions;
	private CharacterController _characterController;

	public void OnMove(InputAction.CallbackContext context)
	{
		Debug.Log("OnMove called");
		_movementInput = context.ReadValue<Vector2>();
		_movementInput.Normalize();
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
			Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y) * _playerSpeed * Time.fixedDeltaTime;
			_characterController.Move(movement);
		}
	}

	public void OnDestroy()
	{
		_inputActions.Gameplay.Disable();
	}
}
