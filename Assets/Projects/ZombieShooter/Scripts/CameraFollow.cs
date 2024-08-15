using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Vector3 _offset;
        private void Start()
        {
            _offset = transform.position - _target.position;
        }

        private void LateUpdate()
        {
            Vector3 newPosition = _target.position + _offset;
            transform.position = newPosition;
        }
    }

}
