using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerModule _playerModule;
    [SerializeField] private EnemyModule _enemyModule;

    private void Start()
    {
        _playerModule.Initialize();
        _enemyModule.Initialize();
    }
}
