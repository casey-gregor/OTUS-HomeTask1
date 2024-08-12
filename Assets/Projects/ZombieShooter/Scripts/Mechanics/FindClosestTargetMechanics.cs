using Atomic.Elements;
using Atomic.Objects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ZombieShooterScripts.Mechanics
{
    public class FindClosestTargetMechanics : IAtomicFixedUpdate
    {
        private IAtomicVariable<GameObject> _target;
        private IAtomicValue<Vector3> _root;
        private IAtomicValue<float> _radius;
        private IAtomicValue<LayerMask> _layerMask;

        public FindClosestTargetMechanics(
            IAtomicVariable<GameObject> target, 
            IAtomicValue<Vector3> root, 
            IAtomicValue<float> radius, 
            IAtomicValue<LayerMask> layerMask)
        {
            _target = target;
            _root = root;
            _radius = radius;
            _layerMask = layerMask;

            //Debug.Log("_target : " + _target.Value);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _target.Value = null;

            var colliders = Physics.OverlapSphere(_root.Value, _radius.Value, _layerMask.Value);

            if (colliders.Length != 0)
            {
                //Debug.Log("colliders : " + colliders.Length);
                float minDistance = float.MaxValue;

                foreach (var collider in colliders)
                {
                    var distance = collider.transform.position - _root.Value;
                    if (distance.magnitude < minDistance)
                    {
                        _target.Value = collider.gameObject;

                        minDistance = distance.magnitude;
                    }
                }
            }
        }
    }
}