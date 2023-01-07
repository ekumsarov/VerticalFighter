using UnityEngine;

namespace VecrticalFighter.Model
{
    public class Turret : SceneObject
    {
        private Transformable _target;
        private Transformable _parent;

        public Turret(Transformable target, Transformable parent)
        {
            _target = target;
            _parent = parent;
            ResetRotation(90f);
        }

        public override void Hit(int amount)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
           if(_target != null)
            {
                Vector2 difference = _target.Position - _parent.Position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Rotate(rotationZ);
            }
        }
    }
}