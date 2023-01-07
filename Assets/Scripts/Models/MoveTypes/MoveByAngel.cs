using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByAngel : MovableDecorator
{
    private float _angel;

    public MoveByAngel(IMovable moveType, float angel) : base(moveType)
    {
        _angel = Mathf.Deg2Rad * angel;
    }

    protected override Vector2 GetPositionInternal(Vector2 position, float deltaTime, float speed)
    {
        float xx = speed * deltaTime * Mathf.Cos(_angel);
        float yy = speed * deltaTime * Mathf.Sin(_angel);
        Vector2 Position = _wrappedMove.GetPosition(position, deltaTime, speed);
        return new Vector2(Position.x + xx, Position.y + yy);
    }
}