using UnityEngine.AI;

public class DamageState : EnemyState
{
    public DamageState(Enemy enemy, NavMeshAgent agent) : base(enemy, agent) { }

    public override void EnterState()
    {
        Agent.isStopped = true;
    }

    public override void ExitState()
    {
        Agent.isStopped = false;
    }
}