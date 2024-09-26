using UnityEngine;

public class PlayerModule : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void Initialize()
    {
        _player.Initialize();
    }
}
