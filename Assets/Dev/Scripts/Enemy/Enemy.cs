using UnityEngine;
using UnityEngine.AI;
using Zenject;

public interface IEnemyBehaviour : IDamageable, IEnemyData
{
    public void Initialize();
    public void Move();
}

public interface IEnemyData
{
    public Transform Transform { get; }
}

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IEnemyBehaviour, IDamageable
{
    [SerializeField] protected int MaxHealth = 100;
    protected IEnemyHealth Health;

    [Inject(Id = "PlayerTransform")] private Transform _playerTransform;
    [Inject] private EnemiesHandler _enemiesHandler;

    private NavMeshAgent _agent;
    private EnemyState _currentState;

    public bool IsDead { get; private set; }
    public Transform Transform => transform;
    public Vector3 PlayerPosition => _playerTransform.position;

    public virtual void Initialize()
    {
        _agent = GetComponent<NavMeshAgent>();
        Health = GetComponentInChildren<IEnemyHealth>();

        if (Health != null)
        {
            Health.Initialize(MaxHealth);
            Health.Death += OnDeath;
        }

        ChangeState(new IdleState(this, _agent));
    }

    public void GetDamage(int value)
    {
        ChangeState(new DamageState(this, _agent));
        Health.Decrease(value);
    }

    private void OnDeath()
    {
        ChangeState(new DeathState(this, _agent));

        IsDead = true;
    }

    private void ChangeState(EnemyState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    public void Move()
    {
        _agent.SetDestination(PlayerPosition);
    }

    public void Despawn()
    {
        _enemiesHandler.RemoveEnemy(this);
        Health.Death -= OnDeath;
    }
}
