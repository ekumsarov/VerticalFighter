using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ExplodePresenter : Presenter
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
            DestroyExplode();
    }

    public void DestroyExplode()
    {
        Destroy(gameObject);
    }
}