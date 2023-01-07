using System;

public class ImmortalDamage : IDamagableType
{
    private HPChanged _listener;
    public HPChanged Listener { set => _listener = value; }
    public bool IsDead(int amount)
    {
        return false;
    }

    public void SetupHP(int amount)
    {
    }

    public void AddHP(int amount) { }
}
