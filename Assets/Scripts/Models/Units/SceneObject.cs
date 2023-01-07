using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VecrticalFighter.Model
{
    public abstract class SceneObject : Transformable, IUpdatable, IDamagable
    {
        public abstract void Update(float deltaTime);

        public abstract void Hit(int amount);
    }
}

