using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnConfig", menuName = "Configs/SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    [SerializeField] private List<EnemyType> _enemyTypes = new List<EnemyType>();

    public Queue<EnemyType> SpawnQueue { get; private set; }
    [field: SerializeField] public int MaxEnemies { get; private set; }
    [field: SerializeField] public float SpawnInterval { get; private set; }
    [field: SerializeField] public float SpawnRadius { get; private set; }

    private void OnEnable()
    {
        SpawnQueue = new Queue<EnemyType>(_enemyTypes);
    }
}