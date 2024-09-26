using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private SpawnConfig _spawnConfig;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Enemy _enemyCommonPrefab;
    [SerializeField] private EnemyArmored _enemyArmoredPrefab;
    [SerializeField] private Transform _spawnPoint;

    public override void InstallBindings()
    {
        #region Player Bindings
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
        Container.Bind<IDamageZone>().To<DamageZone>().AsSingle()
            .WithArguments(_playerConfig.Damage, _playerConfig.AttackCooldown, (int)_playerConfig.MaxTargets);

        Container.Bind<DamageZoneHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IPlayerMovement>().To<PlayerJoystickMove>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Transform>()
            .WithId("PlayerTransform")
            .FromInstance(_playerTransform);

        #endregion

        #region Enemies Bindings
        Container.BindInstance(_spawnConfig).AsSingle();
        Container.Bind<EnemySpawner>().AsSingle();
        Container.Bind<EnemiesHandler>().AsSingle();

        Container.BindMemoryPool<Enemy, EnemyPool<Enemy>>()
            .WithInitialSize(_spawnConfig.MaxEnemies)
            .FromComponentInNewPrefab(_enemyCommonPrefab)
            .UnderTransformGroup("EnemiesContainer");

        Container.BindMemoryPool<EnemyArmored, EnemyPool<EnemyArmored>>()
            .WithInitialSize(_spawnConfig.MaxEnemies)
            .FromComponentInNewPrefab(_enemyArmoredPrefab)
            .UnderTransformGroup("EnemiesContainer");

        Container.BindFactory<EnemyType, Enemy, EnemyFactory>()
            .AsSingle();

        Container.Bind<Transform>()
            .WithId("SpawnPoint")
            .FromInstance(_spawnPoint)
            .WhenInjectedInto<EnemySpawner>();
        #endregion
    }
}