using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArmoredHealth : MonoBehaviour, IEnemyHealth
{
    public Action Death { get; set; }

    [SerializeField] private Image _healthBar;
    [SerializeField] private int _maxArmor = 3;
    [SerializeField] private GameObject _armorPrefab;
    [SerializeField] private Transform _armorContainer;

    private List<GameObject> _armor = new List<GameObject>();
    private Camera _mainCamera;
    private int _maxHealth;
    private int _currentArmor;

    public int Value { get; private set; }
    public int Armor => _currentArmor;

    public void Initialize(int healthValue)
    {
        _mainCamera = Camera.main;
        _maxHealth = healthValue;
        Value = _maxHealth;

        _currentArmor = _maxArmor;

        InitializeArmorView();
        CalculateBarValue();
    }

    private void InitializeArmorView()
    {
        foreach (GameObject armor in _armor)
        {
            Destroy(armor);
        }
        _armor.Clear();

        for (int i = 0; i < _maxArmor; i++)
        {
            GameObject armor = Instantiate(_armorPrefab, _armorContainer);
            armor.SetActive(true);
            _armor.Add(armor);
        }
    }

    public void Decrease(int value)
    {
        if (value < 0)
        {
            Debug.LogError($"Value is less than zero: value = {value}");
            return;
        }

        if (_currentArmor > 0)
        {
            _currentArmor -= 1;

            GameObject armorPiece = _armor.FirstOrDefault();
            if (armorPiece != null)
            {
                armorPiece.SetActive(false);
                _armor.Remove(armorPiece);
            }
        }
        else
        {
            Value -= value;

            if (Value <= 0)
            {
                Value = 0;
                Death?.Invoke();
            }
        }

        CalculateBarValue();
    }

    private void Update()
    {
        RotateBarToPlayer();
        RotateArmorToPlayer();
    }

    private void RotateBarToPlayer()
    {
        if (_healthBar != null && _mainCamera != null)
        {
            _healthBar.transform.LookAt(_healthBar.transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
        }
    }

    private void RotateArmorToPlayer()
    {
        if (_armorContainer != null && _mainCamera != null)
        {
            _armorContainer.transform.LookAt(_healthBar.transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
        }
    }


    private void CalculateBarValue()
    {
        if (_healthBar != null)
        {
            float fillValue = (float)Value / _maxHealth;
            _healthBar.fillAmount = fillValue;
        }
    }
}
