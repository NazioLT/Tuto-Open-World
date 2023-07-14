using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class CharacterMotor : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _jumpVelocity = 6f;
    [SerializeField] private LayerMask _floorSensorMask;

    [SerializeField] private Transform _character;

    private UnityEvent _onJump = new UnityEvent();

    private Vector2 _moveInput = Vector2.zero;
    private float _rotationInput = 0;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _positionCorrection = Vector3.zero;
    private bool _grounded = false;

    private Rigidbody _body;
    private CapsuleCollider _capsuleCollider;

    public bool Grounded => _grounded;
    public float SpeedPercent => Mathf.Abs(_velocity.z) / _moveSpeed;
    public UnityEvent OnJump => _onJump;

    public void MovePerformed(Vector2 inputValue)
    {
        _moveInput = Vector2.ClampMagnitude(inputValue, 1f);
    }

    public void LookPerformed(Vector2 inputValue)
    {
        _rotationInput = inputValue.x;
    }

    public void JumpPerformed(bool performed)
    {
        if (!performed)
            return;

        Jump();
    }

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        _positionCorrection = Vector3.zero;

        SetGrounded();

        ApplyGravity();

        _velocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _moveSpeed + Vector3.up * _velocity.y;

        Vector3 stepDelta = (_character.forward * _velocity.z + _character.up * _velocity.y) * Time.deltaTime + _positionCorrection;

        _body.MovePosition(_body.position + stepDelta);
    }

    private void Update()
    {
        _character.rotation *= Quaternion.Euler(Vector3.up * _rotationInput * Time.deltaTime);
    }

    private void Jump()
    {
        if (!_grounded)
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

        if (_velocity.y > 0f)
            return;

        if (!Physics.SphereCast(transform.position + Vector3.up * (1 + _capsuleCollider.radius / 2), _capsuleCollider.radius, Vector3.down, out RaycastHit hit, 1f, _floorSensorMask))
            return;

        _positionCorrection += hit.point - transform.position;

        _grounded = true;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.color = _grounded ? Color.green : Color.red;

        Vector3 originPos = transform.position + Vector3.up * (1 + _capsuleCollider.radius / 2);

        Gizmos.DrawSphere(originPos, _capsuleCollider.radius);
        Gizmos.DrawSphere(originPos + Vector3.down, _capsuleCollider.radius);
    }
}
