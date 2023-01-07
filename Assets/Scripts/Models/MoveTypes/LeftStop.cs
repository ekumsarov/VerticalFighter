using UnityEngine;

public class LeftStop : MovableDecorator
{
    private float _stopX;
    public LeftStop(IMovable moveType) : base(moveType)
    {
        _stopX = Config.LeftBoardX + Config.Width/4;
    }

    protected override Vector2 GetPositionInternal(Vector2 position, float deltaTime, float speed)
    {
        Vector2 Position = _wrappedMove.GetPosition(position, deltaTime, speed);

        if (Position.x >= _stopX)
            return Position;
        else
            return new Vector2(Position.x + deltaTime * speed, Position.y);
    }
}