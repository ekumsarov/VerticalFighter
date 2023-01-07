using UnityEngine;
using VecrticalFighter.Model;

public class ObstaclePresenter : Presenter
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBorder"))
        {
            Model.Destroy();
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Model.Hit(Config.PlayerBulletDamage);
        }
    }
}