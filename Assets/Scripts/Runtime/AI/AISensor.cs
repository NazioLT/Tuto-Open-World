using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField] private float _visionRange = 5f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private Transform _player;

    private bool _canSeePlayer = false;
    private bool _canAttackPlayer = false;

    public bool CanSeePlayer => _canSeePlayer;
    public bool CanAttackPlayer => _canAttackPlayer;
    public Vector3 PlayerPosition => _player.position;

    private void Update()
    {
        Vector3 playerDelta = _player.position - transform.position;
        float distanceToPlayer = playerDelta.magnitude;

        _canSeePlayer = distanceToPlayer < _visionRange;
        _canAttackPlayer = distanceToPlayer < _attackRange;
    }
}