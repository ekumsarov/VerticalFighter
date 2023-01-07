using UnityEngine;
using VecrticalFighter.Model;
using VecrticalFighter;

public class BonusPresenter : Presenter
{
    [SerializeField] private BonusType _bonusType;
    private PlayerShip _ship;
    private Level _level;


    public void Init(SceneObject model, PlayerShip ship, Level level)
    {
        _ship = ship;
        _level = level;
        base.Init(model);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBorder"))
        {
            Model.Destroy();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if(_bonusType == BonusType.Fuel || _bonusType == BonusType.Coin || _bonusType == BonusType.Star)
                _level.RecievedGlobalBonus(_bonusType);
            else
                _ship.TakeBonus(_bonusType);

            if (_bonusType == BonusType.Star)
                _ship.TakeBonus(_bonusType);

            Model.Destroy();
        }
    }
}