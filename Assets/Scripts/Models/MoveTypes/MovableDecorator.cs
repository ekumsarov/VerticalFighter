using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableDecorator : IMovable
{
    protected readonly IMovable _wrappedMove;

    protected MovableDecorator(IMovable moveType)
    {
        _wrappedMove = moveType;
    }

    public Vector2 GetPosition(Vector2 position, float deltaTime, float speed)
    {
        return GetPositionInternal(position, deltaTime, speed);
    }

    protected abstract Vector2 GetPositionInternal(Vector2 position, float deltaTime, float speed);
}
