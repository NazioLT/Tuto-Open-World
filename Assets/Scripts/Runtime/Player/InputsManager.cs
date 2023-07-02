using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputsManager : MonoBehaviour
{
    private UnityEvent<Vector2> _onMovePerformed = new UnityEvent<Vector2>();
    private UnityEvent<Vector2> _onLookPerformed = new UnityEvent<Vector2>();
    private UnityEvent<bool> _onFirePerformed = new UnityEvent<bool>();
    private UnityEvent<bool> _onJumpPerformed = new UnityEvent<bool>();

    public UnityEvent<Vector2> OnMovePerformed => _onMovePerformed;
    public UnityEvent<Vector2> OnLookPerformed => _onLookPerformed;
    public UnityEvent<bool> OnFirePerformed => _onFirePerformed;
    public UnityEvent<bool> OnJumpPerformed => _onJumpPerformed;

    public void MovePerformed(InputAction.CallbackContext ctx)
    {
        _onMovePerformed.Invoke(ctx.ReadValue<Vector2>());
    }

    public void LookPerformed(InputAction.CallbackContext ctx)
    {
        _onLookPerformed.Invoke(ctx.ReadValue<Vector2>());
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
