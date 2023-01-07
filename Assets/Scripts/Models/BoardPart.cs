using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VecrticalFighter.Model
{
    public class BoardPart : SceneObject
    {
        IMovable _move;

        public BoardPart(Vector2 position)
        {
            _move = MoveFactory.GetMoveType(MoveType.TickMove);
            ResetPosition(position);
        }

        public override void Hit(int amount)
        {
        }

        public override void Update(float deltaTime)
        {
            MoveTo(_move.GetPosition(this.Position, deltaTime, Config.TickSpeed));
        }
    }
}