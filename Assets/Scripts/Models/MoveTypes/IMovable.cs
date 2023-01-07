using UnityEngine;

public interface IMovable 
{
    public Vector2 GetPosition(Vector2 position, float deltaTime, float speed);
}
