using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VecrticalFighter.Model
{
    public class Bonus : SceneObject
    {
        private IMovable _move;

        public Bonus(Vector2 position)
        {
            _move = MoveFactory.GetMoveType(MoveType.TickMove);
            ResetPosition(position);
        }

        public override void Hit(int amount)
        {
        }

        public override void Update(float deltaTime)
        {
            MoveTo(_move.GetPosition(this.Position, deltaTime, 0f));
        }
    }
}

