using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VecrticalFighter.Model
{
    public class Bullet : SceneObject
    {
        IMovable _move;
        float _speed;

        public Bullet(float angel, float speed, Vector2 position)
        {
            _move = MoveFactory.GetMoveByAgnel(angel);
            _speed = speed;
            ResetPosition(position);
        }

        public Bullet(Transformable target, float speed, Vector2 position)
        {
            _move = MoveFactory.GetFollowMove(target);
            _speed = speed;
            ResetPosition(position);
        }

        public override void Hit(int amount)
        {
            Destroy();
        }

        public override void Update(float deltaTime)
        {
            MoveTo(_move.GetPosition(this.Position, deltaTime, _speed));
        }
    }
}