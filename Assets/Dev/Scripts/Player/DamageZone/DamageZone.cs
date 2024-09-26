using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IDamageZone
{
    public void AddTarget(IDamageable target);
    public void RemoveTarget(IDamageable target);
    public void UpdateDamageZone();
}

public class DamageZone : IDamageZone
{
    private readonly float _damage;
    private readonly float _attackCooldown;
    private readonly int _maxTargets;
    private readonly HashSet<IDamageable> _currentTargets = new HashSet<IDamageable>();
    private float _nextAttackTime = 0f;

    public DamageZone(float damage, float attackCooldown, int maxTargets)
    {
        _damage = damage;
        _attackCooldown = attackCooldown;
        _maxTargets = maxTargets;
    }

    public void AddTarget(IDamageable target)
    {
        if (_currentTargets.Count < _maxTargets && !_currentTargets.Contains(target))
        {
            _currentTargets.Add(target);
        }
    }

    public void RemoveTarget(IDamageable target)
    {
        if (_currentTargets.Contains(target))
        {
            _currentTargets.Remove(target);
        }
    }

    public void UpdateDamageZone()
    {
        if (Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + _attackCooldown;
            AttackTargets();
        }
    }

    private void AttackTargets()
    {
        List<IDamageable> deadTargets = _currentTargets.Where(target =>
        {
            target.GetDamage((int)_damage);
            return target.IsDead;
        }).ToList();

        foreach (var deadTarget in deadTargets)
        {
            _currentTargets.Remove(deadTarget);
        }
    }
}