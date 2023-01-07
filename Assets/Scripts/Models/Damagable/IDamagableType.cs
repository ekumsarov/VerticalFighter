using System;

public interface IDamagableType
{
    public bool IsDead(int amount);

    public void SetupHP(int amount);

    public void AddHP(int amount);

    public HPChanged Listener { set; }
}
