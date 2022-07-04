using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MovementModeController : MonoBehaviour
{
    public UnityEvent OnEnterMovementMode;
    public UnityEvent OnExitMovementMode;
    
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void OnEnterMovement(InputValue input)
    {
        if (input.isPressed)
        {
            _playerInput.SwitchCurrentActionMap("Movement");
            OnEnterMovementMode.Invoke();
        }
    }

    public void OnExitMovement(InputValue input)
    {
        if (input.isPressed)
        {
            _playerInput.SwitchCurrentActionMap("NoMovement");
            OnExitMovementMode.Invoke();
        }
    }
}
