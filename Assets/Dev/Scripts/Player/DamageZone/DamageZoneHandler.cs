using UnityEngine;
using Zenject;

[RequireComponent(typeof(SphereCollider))]
public class DamageZoneHandler : MonoBehaviour
{
    [Inject] private IDamageZone _damageZone;
    private SphereCollider _attackCollider;
 
    public void Initialize(float attackRadius)
    {
        _attackCollider.radius = attackRadius;
    }

    public void UpdateDamageZone()
    {
        _damageZone.UpdateDamageZone();
    }

    private void OnEnable()
    {
        _attackCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            _damageZone.AddTarget(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            _damageZone.RemoveTarget(damageable);
        }
    }
}
