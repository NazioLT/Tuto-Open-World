using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private AISensor _sensor = null;
    [SerializeField] private NavMeshAgent _agent = null;

    private void Update()
    {
        if(_sensor.CanAttackPlayer)
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
        _agent.isStopped = true;
    }
}
