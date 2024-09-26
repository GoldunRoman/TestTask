using Zenject;

public class EnemyPool<T> : MonoMemoryPool<T> where T : Enemy
{
    protected override void OnSpawned(T enemy)
    {
        enemy.Initialize();

        base.OnSpawned(enemy);
    }

    protected override void OnDespawned(T enemy)
    {
        base.OnDespawned(enemy);
    }
}