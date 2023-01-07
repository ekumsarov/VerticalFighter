using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VecrticalFighter.Model;

public class PlayerInputRouter 
{
    private PlayerShip _ship;
    private PlayerInputHandler _inputHandler;

    private Vector2 _velocity;

    public void Init(PlayerShip ship, PlayerInputHandler handler)
    {
        _ship = ship;
        _inputHandler = handler;
        _inputHandler.VectorInputChange += _ship.SetVelocity;
    }

    public void Update(float deltaTiem)
    {
        _ship.Update(deltaTiem);
    }
}
