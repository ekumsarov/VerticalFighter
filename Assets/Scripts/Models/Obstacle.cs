using UnityEngine;

namespace VecrticalFighter.Model
{
    public class Obstacle : SceneObject
    {
        private ObstacleType _myType;
        private IMovable _move;
        private IDamagableType _damage;

        public Obstacle(ObstacleType type, Vector2 position)
        {
            _myType = type;
            _move = MoveFactory.GetMoveType(MoveType.TickMove);
            ResetPosition(position);

            if (_myType == ObstacleType.Ruler)
            {
                _damage = DamageFactory.GetMDamageType(DamageType.Simple);
                _damage.SetupHP(5);
            }
        }

        public override void Update(float deltaTime)
        {
            MoveTo(_move.GetPosition(this.Position, deltaTime, 0f));
        }

        public override void Hit(int amount)
        {
            if (_damage != null && _damage.IsDead(amount))
            {
                Destroy();
            }
        }
    }

}