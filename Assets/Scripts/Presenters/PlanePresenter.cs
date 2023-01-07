using UnityEngine;
using VecrticalFighter.Model;
using VecrticalFighter;

public class PlanePresenter : Presenter
{
    [SerializeField] Transform _gun1Point;
    [SerializeField] Transform _gun2Point;

    private Level _level;
    private bool _deadByPlayer = false;
    private PresentersFactory _factory;
    private MoveType _bulletMoveType;

    public void Init(VecrticalFighter.Model.Plane plane, Level level, PresentersFactory factory)
    {
        this.transform.Rotate(new Vector3(0f, 0f, plane.Rotation));
        _level = level;
        _factory = factory;
        base.Init(plane);
        _bulletMoveType = plane.BulletMoveType;

        if (_gun1Point != null)
            plane.OnShoot += Shoot;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _deadByPlayer = true;
            Model.Hit(Config.PlayerBulletDamage);
        }

        if (collision.gameObject.CompareTag("EnemyBorder"))
        {
            Model.Destroy();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            _deadByPlayer = true;
            Model.Hit(1);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Model.Destroy();
        }
    }

    public void Shoot(float speed, float angel)
    {
        _factory.CreateEnemyBullet(angel, speed, _gun1Point.position);
        
        if(_gun2Point != null)
            _factory.CreateEnemyBullet(angel, speed, _gun2Point.position);
    }

    protected override void OnDestroying()
    {
        _factory.CreateExplode(gameObject.transform.position);

        base.OnDestroying();

        if(_deadByPlayer)
            _level.TryAddBonus(transform.position);
    }
}