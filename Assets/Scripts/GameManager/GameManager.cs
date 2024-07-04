using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    public sealed class GameManager : MonoBehaviour, IGameListener, IFixedTickable, ITickable
    {
        public enum State
        {
            Unknown,
            Start,
            Pause,
            Resume,
            Finish
        }

        public State state { get; private set; }

        private List<IGameListener> gameListeners = new();
        private RegisterListenersComponent registerListenersComponent = new RegisterListenersComponent();

        private void Awake()
        {
            IGameListener.RegisterEvent += RegisterEventHandler;
            IGameListener.UnregisterEvent += UnregisterEventHandler;
        }
        [Inject]
        private void Construct(ListenersStorage listenersStorage)
        {
            gameListeners = listenersStorage.gameListeners;
            //Debug.Log("construct of Game Manager");
        }

        private void Start()
        {
            InterateThroughSceneObjects();
            //for (int i = 0; i < gameListeners.Count; i++)
            //{
            //    Debug.Log("gameListener : " + gameListeners[i]);
            //}
        }

        private void RegisterEventHandler(IGameListener gameListener)
        {
            if(!gameListeners.Contains(gameListener))
            {
                gameListeners.Add(gameListener);
                //Debug.Log("added from gameManager " + gameListener);
            }
        }
        private void UnregisterEventHandler(IGameListener gameListener)
        {
            gameListeners.Remove(gameListener);
        }
        //private void FixedUpdate()
        //{
        //    if(!CanUpdate()) return;

        //    for(int i = 0; i < gameListeners.Count; i++)
        //    {
        //        if (gameListeners[i] is IGameFixedUpdateListener fixedUpdateListener)
        //        {
        //            fixedUpdateListener.OnFixedUpdate();
        //        }
        //    }
        //}

        //private void Update()
        //{
        //    if (!CanUpdate()) return;
            
        //    for (int i = 0; i < gameListeners.Count; i++)
        //    {
        //        if (gameListeners[i] is IGameUpdateListener updateListener)
        //        {
        //            updateListener.OnUpdate();
        //        }
        //    }
        //}

        private void SwitchState(State state)
        {
            switch (state)
            {
                case State.Start:
                    for (int i = 0; i < gameListeners.Count; i++)
                    {
                        if (gameListeners[i] is IGameStartListener listener)
                        {
                            listener.OnStart();
                        }
                    }
                    break;
                case State.Pause:
                    for (int i = 0; i < gameListeners.Count; i++)
                    {
                        if (gameListeners[i] is IGamePauseListener listener)
                        {
                            listener.OnPause();
                        }
                    }
                    break;
                case State.Resume:
                    for (int i = 0; i < gameListeners.Count; i++)
                    {
                        if (gameListeners[i] is IGameResumeListener listener)
                        {
                            listener.OnResume();
                        }
                    }
                    break;
                case State.Finish:
                    for (int i = 0; i < gameListeners.Count; i++)
                    {
                        if (gameListeners[i] is IGameFinishListener listener)
                        {
                            listener.OnFinish();
                        }
                    }
                    break;
            }
        }

        public void SetState(State newState)
        {
            if (state != newState)
            {
                SwitchState(newState);
            }
            state = newState;
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            SetState(State.Finish);
        }

        private bool CanUpdate()
        {
            return state is State.Start or State.Resume;
        }

        private void InterateThroughSceneObjects()
        {
            GameObject[] sceneRootObjects = gameObject.scene.GetRootGameObjects();
            foreach (GameObject sceneObject in sceneRootObjects)
            {
                //Debug.Log("sceneObject : " + sceneObject.name);
                RecursiveRegister(sceneObject.transform);
            }
        }

        private void RecursiveRegister(Transform transform)
        {
            registerListenersComponent.RegisterListeners(transform.gameObject);
            foreach(Transform child in transform)
            {
                //Debug.Log($"is child {child.name} active : " + child.gameObject.activeSelf);
                if (!child.gameObject.activeSelf)
                    return;
                //Debug.Log("try to register : " + child.name);
                RecursiveRegister(child);
            }
        }

        public void FixedTick()
        {
            if (!CanUpdate()) return;

            for (int i = 0; i < gameListeners.Count; i++)
            {
                //Debug.Log("gameListener : " + gameListeners[i]);
                if (gameListeners[i] is IGameFixedUpdateListener fixedUpdateListener)
                {
                    fixedUpdateListener.OnFixedUpdate();
                }
            }
            //Debug.Log("end ============ ");
        }

        public void Tick()
        {
            if (!CanUpdate()) return;

            for (int i = 0; i < gameListeners.Count; i++)
            {
                if (gameListeners[i] is IGameUpdateListener updateListener)
                {
                    updateListener.OnUpdate();
                }
            }
        }
    }
}