using UnityEngine;

namespace VecrticalFighter.Model
{
    public class Plane : SceneObject
    {
        private EnemyType _myType;

        private IMovable _move;
        private IDamagableType _damage;
        private float _speed;

        private float _bulletSpeed;
        private float _bulletRecharge;
        private float _currentBulletRecharge;
        private bool _hasGun;

        public delegate void OnShootDelegate(float speed, float angel);
        public OnShootDelegate OnShoot;

        public MoveType BulletMoveType { get; private set; }

        public Turret Turret { get; private set; }

        public Plane(PlaneData data, Transformable _target)
        {
            _myType = data.EnemyType;

            MoveType moveType = data.MoveType[Random.Range(0, data.MoveType.Count)];
            BulletMoveType = moveType;

            Rotate(LookByMoveType(moveType));
            ResetPosition(MoveFactory.GetStartPosition(moveType));
            _move = MoveFactory.GetMoveType(moveType);
            
            _damage = DamageFactory.GetMDamageType(data.DamageType);
            _damage.SetupHP(data.HealthPoints);
            
            _speed = data.MoveSpeed;
            _bulletSpeed = data.BulletSpeed;
            _bulletRecharge = data.BulletRecharge;
            _currentBulletRecharge = _bulletRecharge;
            _hasGun = _bulletRecharge != 0f;

            if(_myType == EnemyType.Zinit || _myType == EnemyType.BlueTank || _myType == EnemyType.BlackTank)
                Turret = new Turret(_target, this);
        }

        public override void Update(float deltaTime)
        {
            MoveTo(_move.GetPosition(this.Position, deltaTime, _speed));

            if(_hasGun)
            {
                _currentBulletRecharge -= deltaTime;
                if(_currentBulletRecharge <= 0f)
                {
                    _currentBulletRecharge = _bulletRecharge;
                    OnShoot?.Invoke(_bulletSpeed, Turret.Rotation);
                }
            }
        }

        public override void Hit(int amount)
        {
            if(_damage.IsDead(amount))
            {
                Destroy();
            }
        }

        #region help 
        private float LookByMoveType(MoveType type)
        {
            float start = RotateSpriteOffset(_myType);

            if (type == MoveType.LeftLinear || type == MoveType.LeftStatic)
                return LookAtLeft(start);

            if (type == MoveType.RightLinear || type == MoveType.RightStatic)
                return LookAtRight(start);

            if (type == MoveType.TopDown)
                return LookDown(start);

            return start;
        }

        private float LookAtRight(float rotate)
        {
            return rotate - 90f;
        }

        private float LookAtLeft(float rotate)
        {
            return rotate + 90f;
        }

        private float LookDown(float rotate)
        {
            return rotate - 180;
        }

        private float RotateSpriteOffset(EnemyType type)
        {
            if (type == EnemyType.Plane1)
                return 180f;

            if (type == EnemyType.Plane2)
                return 270f;

            if (type == EnemyType.BlackTank)
                return 90f;

            return 0f;
        }
        #endregion
    }

}