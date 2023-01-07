using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShipData", menuName = "ShipData")]
public class ShipData : ScriptableObject
{
    [SerializeField] private int _type;
    public int ShipType => _type;

    [SerializeField] private int _shield;
    public int Shield => _shield;

    [SerializeField] private int _speed;
    public int Speed => _speed;

    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private int _fuel;
    public int Fuel => _fuel;

    [SerializeField] private int _recharge;
    public int Recharge => _recharge;
}
