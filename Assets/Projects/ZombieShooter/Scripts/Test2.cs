using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class Test2 : MonoBehaviour
    {
        [SerializeField] private Test test;

        // Use this for initialization
        void Start()
        {
            test.TestEvent.Subscribe(HandleTest);

        }

        private void HandleDirection(Vector3 vector)
        {
            //Debug.Log("getting event");
        }

        // Update is called once per frame
        void Update()
        {
            test.OnUpdate();
        }

        void HandleTest()
        {
            //Debug.Log("getting event");
        }
    }
}