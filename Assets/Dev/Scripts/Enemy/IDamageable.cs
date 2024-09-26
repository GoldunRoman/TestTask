public interface IDamageable
{
    public bool IsDead { get; }
    public void GetDamage(int value);
}
