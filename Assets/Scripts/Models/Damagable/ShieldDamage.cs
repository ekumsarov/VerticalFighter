using System;

public class ShieldDamage : IDamagableType
{
    private bool _isShieldDestroy = false;

    private HPChanged _listener;
    public HPChanged Listener { set => _listener = value; }

    public bool IsDead(int amount)
    {
        if (_isShieldDestroy == true)
            return true;

        _isShieldDestroy = false;
        return false;
    }

    public void SetupHP(int amount)
    {
    }

    public void AddHP(int amount) { }
}
