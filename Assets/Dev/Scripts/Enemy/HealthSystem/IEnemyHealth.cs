using System;

//This interface has 2 realizations. Search "Health" and "ArmoredHealth"
public interface IEnemyHealth
{
    public Action Death { get; set; }
    public int Value { get; }

    public void Initialize(int value);
    public void Decrease(int value);
}