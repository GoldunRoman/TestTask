using UnityEngine;
using UnityEngine.InputSystem;

//I specially write interfaces above their implementation so that it is more convenient for you to search by [ctrl + click].
//But I usually create separate scripts because one interface can have many implementations
public interface IPlayerMovement
{
    public void Initialize();
    public void Move();
    public void SetSpeed(float value);
}

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerJoystickMove : MonoBehaviour, IPlayerMovement
{
    private CharacterController _controller;
    private PlayerInput _playerInput;

    private float _speed;

    public void Initialize()
    {
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void SetSpeed(float value) => _speed = value;

    public void Move()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(30, 45, 0));

        move = isometricMatrix.MultiplyVector(move);
        move.y = 0;

        _controller.Move(move * Time.deltaTime * _speed);
    }
}