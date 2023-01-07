using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : IMovable
{
    public Vector2 GetPosition(Vector2 position, float deltaTime, float speed)
    {
        return position;
    }
}
