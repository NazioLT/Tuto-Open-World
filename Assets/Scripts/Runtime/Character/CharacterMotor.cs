using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class CharacterMotor : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _jumpVelocity = 6f;
    [SerializeField] private LayerMask _floorSensorMask;

    private Vector2 _moveInput = Vector2.zero;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _positionCorrection = Vector3.zero;
    private bool _grounded = false;

    private Rigidbody body;
    private CapsuleCollider capsuleCollider;

    public void MovePerformed(Vector2 inputValue)
    {
        _moveInput = Vector2.ClampMagnitude(inputValue, 1f);
    }

    public void JumpPerformed(bool performed)
    {
        if (!performed)
            return;

        Jump();
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        _positionCorrection = Vector3.zero;

        SetGrounded();

        ApplyGravity();

        _velocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _moveSpeed + Vector3.up * _velocity.y;

        body.MovePosition(body.position + _velocity * Time.deltaTime + _positionCorrection);
    }

    private void Jump()
    {
        if(!_grounded)
            return;

        _velocity.y += _jumpVelocity;
    }

    private void ApplyGravity()
    {
        if (_grounded)
        {
            _velocity.y = 0f;
            return;
        }

        _velocity.y += Physics.gravity.y * Time.deltaTime;
    }

    private void SetGrounded()
    {
        _grounded = false;

        if(_velocity.y > 0f)
            return;

        if (!Physics.SphereCast(transform.position + Vector3.up * (1 + capsuleCollider.radius / 2), capsuleCollider.radius, Vector3.down, out RaycastHit hit, 1f, _floorSensorMask))
            return;

        _positionCorrection += hit.point - transform.position;

        _grounded = true;
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return;

        Gizmos.color = _grounded ? Color.green : Color.red;

        Vector3 originPos = transform.position + Vector3.up * (1 + capsuleCollider.radius / 2);

        Gizmos.DrawSphere(originPos, capsuleCollider.radius);
        Gizmos.DrawSphere(originPos + Vector3.down, capsuleCollider.radius);
    }
}
