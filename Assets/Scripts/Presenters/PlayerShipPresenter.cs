using System.Collections;
using UnityEngine;
using VecrticalFighter.Model;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerShipPresenter : Presenter
{
    [SerializeField] private Transform _firstGun;
    [SerializeField] private Transform _secondGun;
    [SerializeField] private Transform _thirdGun;

    private SpriteRenderer _sprite;
    private bool _isReseting = false;
    private PresentersFactory _factory;
    private PlayerShip _ship;

    private float _halfSizeX;
    private float _halfSizeY;

    public void Init(PlayerShip ship, PresentersFactory factory)
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _ship = ship;
        _ship.OnResetEnd += ResetEnd;
        _ship.OnResetStart += ResetStart;
        _ship.OnShoot += OnShoot;
        _ship.Moved += CheckBoarder;

        _halfSizeX = _sprite.bounds.size.x / 2;
        _halfSizeY = _sprite.bounds.size.y / 2;

        _factory = factory;

        base.Init(ship);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Model.Hit(1);
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Model.Hit(1);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Model.Hit(1);
        }
    }

    public void CheckBoarder()
    {
        if (Model.Position.x - _halfSizeX <= Config.LeftBoardX)
            _ship.PositionCollision(new Vector2(Config.LeftBoardX + _halfSizeX, Model.Position.y));

        if (Model.Position.x + _halfSizeX >= Config.Width/2)
            _ship.PositionCollision(new Vector2(Config.Width/2 - _halfSizeX, Model.Position.y));

        if (Model.Position.y + _halfSizeY >= Config.Height / 2)
            _ship.PositionCollision(new Vector2(Model.Position.x, Config.Height / 2 - _halfSizeY));

        if (Model.Position.y - _halfSizeY <= -Config.Height / 2)
            _ship.PositionCollision(new Vector2(Model.Position.x, -Config.Height / 2 +  _halfSizeY));
    }

    public void ResetStart()
    {
        _isReseting = true;
        StartCoroutine(Blink());
    }

    public void ResetEnd()
    {
        _isReseting = false;
    }

    public void OnShoot(float speed, int GunPosition)
    {
        if (GunPosition == 1)
            _factory.CreatePlayerBullet(speed, _firstGun.position);

        if (GunPosition == 2)
            _factory.CreatePlayerBullet(speed, _secondGun.position);

        if (GunPosition == 3)
            _factory.CreatePlayerBullet(speed, _thirdGun.position);
    }

    private bool increasing;
    IEnumerator Blink()
    {
        yield return null;

        Color color;

        while (_isReseting)
        {
            color = _sprite.color;
            float a = color.a;
            float t = Time.deltaTime;
            if (a >= 1) increasing = false;
            if (a <= 0.3) increasing = true;
            a = increasing ? a += t * 2f * 2 : a -= t * 2f;
            color.a = a;
            _sprite.color = color;
            yield return null;
        }

        yield return null;

        color = _sprite.color;
        color.a = 1;
        _sprite.color = color;
    }

    protected override void OnDestroying()
    {
        _ship.OnResetEnd -= ResetEnd;
        _ship.OnResetStart -= ResetStart;
        _ship.OnShoot -= OnShoot;
        base.OnDestroying();
    }
}
