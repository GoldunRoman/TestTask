using UnityEngine;
using UnityEngine.AI;

public class IdleState : EnemyState
{
    private float _moveCooldown = 1f;
    private float _nextMoveTime;
    private float _maxDistance = 1f;
    private float _randomMoveRadius = 0.5f;

    public IdleState(Enemy enemy, NavMeshAgent agent) : base(enemy, agent) { }

    public override void EnterState()
    {
        MoveToRandomPosition();
    }

    public override void UpdateState()
    {
        if (Time.time >= _nextMoveTime)
        {
            MoveToRandomPosition();
        }
    }

    private void MoveToRandomPosition()
    {
        if (Agent.isActiveAndEnabled)
        {
            _nextMoveTime = Time.time + _moveCooldown;

            Vector3 randomDirection = Random.insideUnitSphere * _randomMoveRadius;
            randomDirection += Enemy.transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, _maxDistance, NavMesh.AllAreas))
            {
                Agent.SetDestination(hit.position);
            }
        }
    }

    public override void ExitState()
    {
        Agent.isStopped = true;
    }
}