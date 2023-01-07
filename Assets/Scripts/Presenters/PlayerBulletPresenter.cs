using UnityEngine;
using VecrticalFighter.Model;

public class PlayerBulletPresenter : Presenter
{
    private PresentersFactory _factory;

    public void Init(SceneObject model, PresentersFactory factory)
    {
        _factory = factory;
        base.Init(model);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBorder"))
        {
            Model.Destroy();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Model.Destroy();
            _factory.CreateBulletBlow(transform.position);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Model.Destroy();
            _factory.CreateBulletBlow(transform.position);
        }
    }
}
