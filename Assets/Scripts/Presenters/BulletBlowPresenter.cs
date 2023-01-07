using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BulletBlowPresenter : Presenter
{
    [SerializeField] private Animator _animator;
    private float destroyTime = 1f;

    public void Init(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        enabled = true;
    }

    private void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
            DestroyBlow();
    }

    public void DestroyBlow()
    {
        Destroy(gameObject);
    }
}
