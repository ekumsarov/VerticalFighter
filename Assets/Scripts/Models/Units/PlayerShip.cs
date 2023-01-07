using UnityEngine;
using System;

namespace VecrticalFighter.Model
{
    public class PlayerShip : SceneObject, HPChanged
    {
        private IDamagableType _damage;

        private float _speed = 4f;
        private Vector2 _velocity;
        private Vector2 _resetPosition;

        private bool _reseting = false;
        private float _resetTimer = 3f;
        private float _currentResetTimer = 0f;

        private PlayerGun _gun;

        public event Action OnResetStart;
        public event Action OnResetEnd;

        public delegate void OnShootDelegate(float speed, int gunPosition);
        public OnShootDelegate OnShoot;

        public delegate void OnHPChanged(int currentHP);
        public OnHPChanged OnHpChanged;

        public PlayerShip(PlayerDataReader data, Vector2 position)
        {
            _resetPosition = position;
            ResetPosition(position);
            _damage = new SimpleDamage();
            _damage.SetupHP(3);
            _damage.Listener = this;
            _gun = new PlayerGun(this, data.Recharge);
            _speed = 3f + data.Speed * 0.5f;

        }

        private void Move(float deltaTime)
        {
            Vector2 newPosition = Position + (_velocity * (_speed * deltaTime));
            MoveTo(newPosition);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public void PositionCollision(Vector2 position)
        {
            ResetPosition(position);
        }

        public override void Update(float deltaTime)
        {
            if(_reseting)
            {
                _currentResetTimer += deltaTime;
                if(_currentResetTimer >= _resetTimer)
                {
                    _currentResetTimer = 0f;
                    _reseting = false;
                    OnResetEnd?.Invoke();
                }
            }

            _gun.Update(deltaTime);
            Move(deltaTime);
        }

        public override void Hit(int amount)
        {
            if(_reseting == false)
            {
                _reseting = true;
                ResetPosition(_resetPosition);
                Move(0);
                _gun.ResetGun();

                OnResetStart?.Invoke();

                if (_damage.IsDead(amount))
                    Destroy();
            }
        }

        public void TakeBonus(BonusType bonusType)
        {
            if(bonusType == BonusType.Gun1 || bonusType == BonusType.Gun2 || bonusType == BonusType.Gun3)
            {
                _gun.UpgradeByBonus(bonusType);
            }
            else if(bonusType == BonusType.Heart)
            {
                _damage.AddHP(1);
            }
            else if(bonusType == BonusType.Star)
            {
                _reseting = true;
                OnResetStart?.Invoke();
            }
        }

        public void Shoot(float bulletSpeed, int gunPosition)
        {
            OnShoot?.Invoke(bulletSpeed, gunPosition);
        }

        public void HPChanged(int currentHP)
        {
            OnHpChanged?.Invoke(currentHP);
        }
    }
}