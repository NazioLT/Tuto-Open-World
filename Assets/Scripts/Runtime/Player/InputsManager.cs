using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputsManager : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 5f;
    private const float GAMEPAD_SENSITIVITY_FACTOR = 40f;

    private UnityEvent<Vector2> _onMovePerformed = new UnityEvent<Vector2>();
    private UnityEvent<Vector2> _onLookPerformed = new UnityEvent<Vector2>();
    private UnityEvent<bool> _onFirePerformed = new UnityEvent<bool>();
    private UnityEvent<bool> _onJumpPerformed = new UnityEvent<bool>();

    private PlayerInput _playerInput;

    public UnityEvent<Vector2> OnMovePerformed => _onMovePerformed;
    public UnityEvent<Vector2> OnLookPerformed => _onLookPerformed;
    public UnityEvent<bool> OnFirePerformed => _onFirePerformed;
    public UnityEvent<bool> OnJumpPerformed => _onJumpPerformed;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void MovePerformed(InputAction.CallbackContext ctx)
    {
        _onMovePerformed.Invoke(ctx.ReadValue<Vector2>());
    }

    public void LookPerformed(InputAction.CallbackContext ctx)
    {
        float factor = _playerInput.currentControlScheme == "Gamepad" ? GAMEPAD_SENSITIVITY_FACTOR : 1f;
        factor *= _sensitivity;
        
        _onLookPerformed.Invoke(ctx.ReadValue<Vector2>() * factor);
    }

    public void FirePerformed(InputAction.CallbackContext ctx)
    {
        _onFirePerformed.Invoke(ctx.performed);
    }

    public void JumpPerformed(InputAction.CallbackContext ctx)
    {
        _onJumpPerformed.Invoke(ctx.performed);
    }
}
