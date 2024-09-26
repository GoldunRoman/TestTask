using System;
using Zenject;

public class EnemyFactory : PlaceholderFactory<EnemyType, Enemy>
{
    private readonly EnemyPool<Enemy> _commonPool;
    private readonly EnemyPool<EnemyArmored> _armoredPool;

    [Inject]
    public EnemyFactory(EnemyPool<Enemy> commonPool, EnemyPool<EnemyArmored> armoredPool)
    {
        _commonPool = commonPool;
        _armoredPool = armoredPool;
    }

    public override Enemy Create(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Common:
                return _commonPool.Spawn();
            case EnemyType.Armored:
                return _armoredPool.Spawn();

            default:
                throw new ArgumentException($"Unknown enemy type: {enemyType}");
        }
    }
}
