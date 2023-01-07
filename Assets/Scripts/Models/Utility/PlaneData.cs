using VecrticalFighter;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New PlaneData", menuName = "PlaneData")]
public class PlaneData : ScriptableObject
{
    [SerializeField] private EnemyType _type;
    public EnemyType EnemyType => _type;

    [SerializeField] private List<MoveType> _moveType;
    public List<MoveType> MoveType => _moveType;

    [SerializeField] private DamageType _damageType;
    public DamageType DamageType => _damageType;

    [SerializeField] private int _healthPoints;
    public int HealthPoints => _healthPoints;

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    [SerializeField] private float _bulletSpeed;
    public float BulletSpeed => _bulletSpeed;

    [SerializeField] private float _bulletRecharge;
    public float BulletRecharge => _bulletRecharge;
}
