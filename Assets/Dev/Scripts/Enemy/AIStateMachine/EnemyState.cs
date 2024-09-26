using UnityEngine.AI;

public interface IState
{
    public void EnterState();
    public void ExitState();
    public void UpdateState();
}

public class EnemyState : IState
{
    protected Enemy Enemy;
    protected NavMeshAgent Agent;

    public EnemyState(Enemy enemy, NavMeshAgent agent)
    {
        Enemy = enemy;
        Agent = agent;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateState() { }
}