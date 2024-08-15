using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class RotateOnMouseCoursorMechanics : IAtomicUpdate
    {
        private Camera _camera;
        private IAtomicValue<Vector3> _root;
        private AtomicObject _entity;

        public RotateOnMouseCoursorMechanics(Camera camera, IAtomicValue<Vector3> root, AtomicObject entity)
        {
            _camera = camera;
            _root = root;
            _entity = entity;
        }


        public void OnUpdate(float deltaTime)
        {

            Vector3 mouseScreenPosition = Input.mousePosition;

            Ray ray = _camera.ScreenPointToRay(mouseScreenPosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(ray, out rayLength))
            {
                Vector3 pointLook = ray.GetPoint(rayLength);
                Vector3 direction = pointLook - _root.Value;
                direction.y = 0;

                _entity.GetVariable<Vector3>(CharacterAPIKeys.ROTATE_DIRECTION).Value = direction;

            }
        }
    }
}