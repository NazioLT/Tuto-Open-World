using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private AISensor _sensor = null;
    [SerializeField] private NavMeshAgent _agent = null;

    private void Update()
    {
        if (_sensor.CanAttackPlayer)
        {
            UpdateAttack();
            return;
        }

        if (_sensor.CanSeePlayer)
        {
            UpdateHunt();
            return;
        }

        UpdateNeutral();
    }

    private void UpdateAttack()
    {
        print("Attacking");
        _agent.isStopped = true;
    }

    private void UpdateHunt()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_sensor.PlayerPosition);
    }

    private void UpdateNeutral()
    {
        _agent.isStopped = false;

        if (Vector3.Distance(_agent.destination, _agent.transform.position) > 1f)
            return;

        Vector3 newDestination = GetNewPatrolTarget();
        _agent.SetDestination(newDestination);
    }

    private Vector3 GetNewPatrolTarget()
    {
        NavMeshHit _hit = new NavMeshHit();
        for (var i = 0; i < 1000; i++)//Max 1000 essais
        {
            Vector3 _randomDirection = _agent.transform.position + Random.insideUnitSphere * 10f;

            NavMesh.SamplePosition(_randomDirection, out _hit, 1f, 1);

            if (_hit.hit) return _hit.position;
        }

        return _agent.transform.position;
    }
}
