using VecrticalFighter.Model;

namespace VecrticalFighter
{
    public class PlayerGun
    {
        private PlayerShip _ship;

        private float _bulletSpeed;
        private float _bulletRecharge;
        private float _currentBulletRecharge;

        private BonusType _currentBonusType = BonusType.Gun1;
        private int _gunLevel = 1;

        public PlayerGun(PlayerShip ship, float rechargeTime)
        {
            _baseBulletRecharge = 1.8f - 0.2f * rechargeTime;

            _ship = ship;
            _bulletSpeed = _baseBulletSpeed;
            _bulletRecharge = _baseBulletRecharge;
            _currentBulletRecharge = 0;
            _gunLevel = 1;
        }

        public void UpgradeByBonus(BonusType bonusType)
        {
            SetByBonusType(bonusType);
        }

        public void ResetGun()
        {
            _gunLevel = 1;
            _bulletSpeed = _baseBulletSpeed;
            _bulletRecharge = _baseBulletRecharge;
            _currentBonusType = BonusType.Gun1;
        }

        public void Update(float deltaTime)
        {
            _currentBulletRecharge += deltaTime;
            if(_currentBulletRecharge >= _bulletRecharge)
            {
                _currentBulletRecharge = 0f;

                if(_currentBonusType == BonusType.Gun1)
                    _ship.Shoot(_bulletSpeed, 1);
                else if(_currentBonusType == BonusType.Gun2)
                {
                    _ship.Shoot(_bulletSpeed, 2);
                    _ship.Shoot(_bulletSpeed, 3);
                }
                else
                {
                    _ship.Shoot(_bulletSpeed, 1);
                    _ship.Shoot(_bulletSpeed, 2);
                    _ship.Shoot(_bulletSpeed, 3);
                }
            }
        }

        private void SetByBonusType(BonusType bonusType)
        {
            if(bonusType == _currentBonusType)
            {
                _gunLevel += 1;
                if(_gunLevel > _maxGunLevel)
                {
                    _gunLevel = _maxGunLevel;
                    return;
                }

                _bulletRecharge -= 0.2f;
                _bulletSpeed += 0.3f;
            }
            else
            {
                _gunLevel = 1;
                _bulletSpeed = _baseBulletSpeed;
                _bulletRecharge = _baseBulletRecharge;
                _currentBonusType = bonusType;
                return;
            }
        }

        private float _baseBulletSpeed = 5f;
        private float _baseBulletRecharge = 1.4f;
        private int _maxGunLevel = 5;
    }
}

