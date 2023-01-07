using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuel 
{
    private const int _fuelAddBonus = 30;

    private int fuelMeterDetect = 3;
    private int _curentMeterDetect = 0;

    private int _fuel;
    private int _maxFuel;

    public int MaxFuel => _maxFuel;

    public delegate void FuelChanged(int current);
    public FuelChanged OnChanged;

    public void Init(int fuelPoint)
    {
        _maxFuel = 100 + fuelPoint * 50;
        _fuel = _maxFuel;
    }

    public void MeterDetect()
    {
        _fuel -= 2;

        OnChanged?.Invoke(_fuel);
    }

    public void RecivedFuel()
    {
        _fuel += _fuelAddBonus;

        if (_fuel >= _maxFuel)
            _fuel = _maxFuel;

        OnChanged?.Invoke(_fuel);
    }
}
