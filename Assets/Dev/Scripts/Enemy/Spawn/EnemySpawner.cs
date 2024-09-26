using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnemySpawner
{
    [Inject(Id = "SpawnPoint")] private readonly Transform _spawnPoint;

    private readonly EnemyFactory _enemyFactory;
    private readonly EnemiesHandler _enemiesHandler;
    private readonly SpawnConfig _spawnConfig;
    private Queue<EnemyType> _originalQueue;

    private bool _isSpawning = false;

    [Inject]
    public EnemySpawner(EnemyFactory enemyFactory, EnemiesHandler enemiesHandler, SpawnConfig spawnConfig)
    {
        _enemyFactory = enemyFactory;
        _enemiesHandler = enemiesHandler;
        _spawnConfig = spawnConfig;

        _originalQueue = new Queue<EnemyType>(_spawnConfig.SpawnQueue);
    }

    public async void StartSpawning()
    {
        if (_isSpawning)
            return;

        _isSpawning = true;

        for (int i = 0; i < _spawnConfig.MaxEnemies; i++)
        {
            SpawnEnemyFromQueue();
        }

        await MaintainEnemyCount();
    }

    private async Task MaintainEnemyCount()
    {
        while (_isSpawning)
        {
            if (_enemiesHandler.Enemies.Count < _spawnConfig.MaxEnemies)
            {
                SpawnEnemyFromQueue();
            }
            await Task.Delay((int)(_spawnConfig.SpawnInterval * 1000));
        }
    }

    private void SpawnEnemyFromQueue()
    {
        if (_spawnConfig.SpawnQueue.Count == 0)
        {
            RefillSpawnQueue();
        }

        if (_spawnConfig.SpawnQueue.Count > 0)
        {
            EnemyType enemyType = _spawnConfig.SpawnQueue.Dequeue();
            IEnemyBehaviour enemy = _enemyFactory.Create(enemyType);

            Vector3 spawnPosition = GetRandomPositionWithinRadius();

            enemy.Transform.SetParent(_spawnPoint);
            enemy.Transform.localPosition = new Vector3(spawnPosition.x, enemy.Transform.position.y, spawnPosition.z);

            _enemiesHandler.AddEnemy(enemy);
        }
    }

    private void RefillSpawnQueue()
    {
        foreach (var enemyType in _originalQueue)
        {
            _spawnConfig.SpawnQueue.Enqueue(enemyType);
        }
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        Vector2 randomPoint = Random.insideUnitCircle * _spawnConfig.SpawnRadius;
        return new Vector3(randomPoint.x, 0, randomPoint.y);
    }
}
