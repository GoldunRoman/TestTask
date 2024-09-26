using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IEnemyHealth
{
    public Action Death { get; set; }

    [SerializeField] private Image _healthBar;

    private Camera _mainCamera;
    private int _maxValue;
    public int Value { get; private set; }

    public void Initialize(int value)
    {
        _mainCamera = Camera.main;
        Value = value;
        _maxValue = value;

        CalculateBarValue();
    }

    private void Update()
    {
        RotateBarToPlayer();
    }

    public void Decrease(int value)
    {
        if (value < 0)
            Debug.LogError($"Value is less than zero: value = {value}");

        Value -= value;

        if (Value <= 0)
        {
            Value = 0;
            Death?.Invoke();
        }

        CalculateBarValue();
    }

    private void RotateBarToPlayer()
    {
        if (_healthBar != null && _mainCamera != null)
        {
            _healthBar.transform.LookAt(_healthBar.transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
        }
    }

    private void CalculateBarValue()
    {
        if (_healthBar != null)
        {
            float fillValue = (float)Value / _maxValue;
            _healthBar.fillAmount = fillValue;
        }
    }
}