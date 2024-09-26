using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject] private PlayerConfig _config;
    [Inject] private DamageZoneHandler _damageZoneHandler;
    [Inject] private IPlayerMovement _playerMovement;

    public void Initialize()
    {
        InitializeMovement();
        _damageZoneHandler.Initialize(_config.AttackRadius);
    }

    private void InitializeMovement()
    {
        _playerMovement.SetSpeed(_config.Speed);
        _playerMovement.Initialize();
    }

    private void Update()
    {
        _damageZoneHandler.UpdateDamageZone();
        _playerMovement.Move();
    }
}