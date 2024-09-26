using UnityEngine;

public class EnemyArmored : Enemy
{
    [SerializeField] private int _initialArmor = 3;

    public override void Initialize()
    {
        base.Initialize();

        Health = GetComponentInChildren<ArmoredHealth>();

        if(Health != null)
        {
            Health.Initialize(MaxHealth);
        }
    }
}
