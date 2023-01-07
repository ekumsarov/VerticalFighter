using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDamage : IDamagableType
{
    private int _healthPoints = 1;

    private HPChanged _listener;
    public HPChanged Listener { set => _listener = value; }

    public bool IsDead(int amount)
    {
        _healthPoints -= 1;
        _listener?.HPChanged(_healthPoints);
        if (_healthPoints <= 0)
            return true;

        return false;
    }

    public void SetupHP(int amount)
    {
        _healthPoints = amount;
        _listener?.HPChanged(_healthPoints);
    }

    public void AddHP(int amount)
    {
        _healthPoints += amount;
        _listener?.HPChanged(_healthPoints);
    }
}
