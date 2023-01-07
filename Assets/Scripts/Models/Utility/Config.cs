using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VecrticalFighter
{
    public enum MoveType
    {
        TickMove,
        LeftLinear,
        LeftStatic,
        LeftStop,
        RightLinear,
        RightStatic,
        RightStop,
        TopDown,
        DownTop,
        Bezier,
        Follow,
        ByAngel
    }

    public enum DamageType
    {
        Simple,
        Shield,
        Immortal
    }

    public enum EnemyType
    {
        Plane1,
        Plane2,
        Plane3,
        Zinit,
        BlueTank,
        BlackTank
    }

    public enum ObstacleType
    {
        Ruler,
        Pin,
        Eraser
    }

    public enum BonusType
    {
        Gun1,
        Gun2,
        Gun3,
        Heart,
        Fuel,
        Coin, 
        Star
    }
}

public static class Config 
{
    public static float TickSpeed => _level.TickSpeed;

    private static float _boardWidth;
    public static float Width => _boardWidth;

    private static float _boardHeight;
    public static float Height => _boardHeight;

    private static Level _level;

    private static float _leftBoardX;
    private static float _rightBoardX;

    private static int _playerBulletDamage;
    public static int PlayerBulletDamage => _playerBulletDamage;

    public static float LeftBoardX => _leftBoardX;

    public static float RightBoardX => _rightBoardX;

    private static int _coins = 0;
    public static int Coins => _coins;

    public static void InitData(Level level, CameraViewportHandler cameraHadler, SpriteRenderer board, SpriteRenderer rightBoard)
    {
        _boardWidth = cameraHadler.Width;
        _boardHeight = cameraHadler.Height;
        _leftBoardX = board.bounds.center.x + board.bounds.size.x / 2;
        _rightBoardX = rightBoard.bounds.center.x - rightBoard.bounds.size.x / 2;
        _level = level;
    }

    public static void InitPlayerData(PlayerDataReader data)
    {
        _playerBulletDamage = data.Damage;
    }

    public static void AddCoin()
    {
        _coins += 1;
    }
}
