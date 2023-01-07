using System;
using UnityEngine;

namespace VecrticalFighter.Model
{
    public abstract class Transformable
    {
        public virtual Vector2 Position { get; private set; }
        public virtual float Rotation { get; private set; }

        public event Action Moved;
        public event Action Rotated;
        public event Action Destroying;

        public void MoveTo(Vector2 position)
        {
            Position = position;
            Moved?.Invoke();
        }

        public void Rotate(float delta)
        {
            Rotation = Mathf.Repeat(delta, 360);
            Rotated?.Invoke();
        }

        public void Destroy()
        {
            Destroying?.Invoke();
        }

        protected void ResetPosition(Vector2 position)
        {
            Position = position;
        }

        protected void ResetRotation(float rotate)
        {
            Rotation = rotate;
        }
    }
}