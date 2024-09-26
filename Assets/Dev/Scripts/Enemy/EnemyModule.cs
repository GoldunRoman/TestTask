using UnityEngine;
using Zenject;

public class EnemyModule : MonoBehaviour
{
    [Inject] private EnemySpawner _spawner;

    public void Initialize()
    {
        _spawner.StartSpawning();
    }
}
