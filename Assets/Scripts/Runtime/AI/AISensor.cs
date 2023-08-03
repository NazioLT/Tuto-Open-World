using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField] private RangeAngleDetection _visionDetection = new RangeAngleDetection(5f, 180f);
    [SerializeField] private  RangeAngleDetection _attackDetection = new RangeAngleDetection(2f, 90f);
    [SerializeField] private Transform _player;

    private bool _canSeePlayer = false;
    private bool _canAttackPlayer = false;

    public bool CanSeePlayer => _canSeePlayer;
    public bool CanAttackPlayer => _canAttackPlayer;
    public Vector3 PlayerPosition => _player.position;

    private void Update()
    {
        _canSeePlayer = _visionDetection.IsAngleRangeDetected(transform.position, _player.position, transform.forward);
        _canAttackPlayer = _attackDetection.IsAngleRangeDetected(transform.position, _player.position, transform.forward);
    }
}