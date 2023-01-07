using VecrticalFighter.Model;
using UnityEngine;

public class TurretUnitPresenter : PlanePresenter
{
    [SerializeField] private TurretPresenter _turretPresenter;

    public void Init(Turret turret)
    {
        _turretPresenter.Init(turret);
    }
}
