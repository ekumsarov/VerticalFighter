using VecrticalFighter.Model;
using UnityEngine;
using System;

public class Presenter : MonoBehaviour
{
    private SceneObject _model;

    private IUpdatable _updatable = null;

    protected SceneObject Model => _model;

    public delegate void PresenterDestroyDelegate(Presenter presenter);
    public PresenterDestroyDelegate PresenterDestroyed;

    public virtual void Init(SceneObject model)
    {
        _model = model;

        if (_model is IUpdatable)
            _updatable = (IUpdatable)_model;

        enabled = true;

        OnMoved();

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_model == null)
            return;

        _model.Moved += OnMoved;
        _model.Destroying += OnDestroying;
        _model.Rotated += OnRotate;
    }

    private void OnDisable()
    {
        if (_model == null)
            return;

        _model.Moved -= OnMoved;
        _model.Destroying -= OnDestroying;
        _model.Rotated -= OnRotate;
    }

    private void Update() => _updatable?.Update(Time.deltaTime);

    private void OnMoved()
    {
        transform.position = _model.Position;
    }

    private void OnRotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _model.Rotation);
    }

    public void DestroyByBonus()
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroying()
    {
        PresenterDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
