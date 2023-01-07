using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMove : MovableDecorator
{
    public TopDownMove(IMovable moveType) : base(moveType)
    {
    }

    protected override Vector2 GetPositionInternal(Vector2 position, float deltaTime, float speed)
    {
        Vector2 Position = _wrappedMove.GetPosition(position, deltaTime, speed);
        return new Vector2(Position.x, Position.y - deltaTime * speed);
    }
}