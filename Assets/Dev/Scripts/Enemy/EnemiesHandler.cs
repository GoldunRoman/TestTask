using System.Collections.Generic;

public class EnemiesHandler
{
    private List<IEnemyBehaviour> _enemies = new List<IEnemyBehaviour>();
    public List<IEnemyBehaviour> Enemies => _enemies;

    public void AddEnemy(IEnemyBehaviour enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(IEnemyBehaviour enemy)
    {
        _enemies.Remove(enemy);
    }
}