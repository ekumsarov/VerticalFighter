using VecrticalFighter;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New LevelData", menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _meterEnemy;
    public int MeterEnemy => _meterEnemy;

    [SerializeField] private int _meterObstacle;
    public int MeterObstacle => _meterObstacle;

    [SerializeField] private int _chanceEnemy;
    public int ChanceEnemy => _chanceEnemy;

    [SerializeField] private int _chanceObstacle;
    public int ChanceObstacle => _chanceObstacle;

    [SerializeField] private int _chanceBonus;
    public int ChanceBonus => _chanceBonus;

    [SerializeField] private int _enemyTypes;
    public int EnemyTypes => _enemyTypes;

    [SerializeField] private float _tickSpeed;
    public float TickSpeed => _tickSpeed;

    [SerializeField] private int _nextLevelMeter;
    public int NextLevelMeter => _nextLevelMeter;
}