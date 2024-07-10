using UnityEngine;
using Zenject;

namespace ShootEmUp
{

    public sealed class GameManager : 
        MonoBehaviour, 
        IGameListener, 
        IFixedTickable, 
        ITickable
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

        private ListenersStorage listenersStorage;
        private RegisterListenersComponent registerListenersComponent = new RegisterListenersComponent();

        private void Awake()
        {
            IGameListener.RegisterEvent += RegisterEventHandler;
            IGameListener.UnregisterEvent += UnregisterEventHandler;
        }
        [Inject]
        private void Construct(ListenersStorage listenersStorage)
        {
            this.listenersStorage = listenersStorage;
        }

        private void Start()
        {
            InterateThroughSceneObjects();
        }

        private void InterateThroughSceneObjects()
        {
            GameObject[] sceneRootObjects = gameObject.scene.GetRootGameObjects();
            foreach (GameObject sceneObject in sceneRootObjects)
            {
                RecursiveRegister(sceneObject.transform);
            }
        }

        private void RegisterEventHandler(IGameListener gameListener)
        {
            if(!this.listenersStorage.gameListeners.Contains(gameListener))
            {
                this.listenersStorage.gameListeners.Add(gameListener);
            }
        }
        private void UnregisterEventHandler(IGameListener gameListener)
        {
            this.listenersStorage.gameListeners.Remove(gameListener);
        }

        private void SwitchState(State state)
        {
            switch (state)
            {
                case State.Start:
                    for (int i = 0; i < this.listenersStorage.gameListeners.Count; i++)
                    {
                        if (this.listenersStorage.gameListeners[i] is IGameStartListener listener)
                        {
                            listener.OnStart();
                        }
                    }
                    break;
                case State.Pause:
                    for (int i = 0; i < this.listenersStorage.gameListeners.Count; i++)
                    {
                        if (this.listenersStorage.gameListeners[i] is IGamePauseListener listener)
                        {
                            listener.OnPause();
                        }
                    }
                    break;
                case State.Resume:
                    for (int i = 0; i < this.listenersStorage.gameListeners.Count; i++)
                    {
                        if (this.listenersStorage.gameListeners[i] is IGameResumeListener listener)
                        {
                            listener.OnResume();
                        }
                    }
                    break;
                case State.Finish:
                    for (int i = 0; i < this.listenersStorage.gameListeners.Count; i++)
                    {
                        if (this.listenersStorage.gameListeners[i] is IGameFinishListener listener)
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

       

        private void RecursiveRegister(Transform transform)
        {
            registerListenersComponent.RegisterListeners(transform.gameObject);
            foreach(Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                    return;
                RecursiveRegister(child);
            }
        }

        public void FixedTick()
        {
            if (!CanUpdate()) return;

            for (int i = 0; i < this.listenersStorage.gameListeners.Count; i++)
            {

                if (this.listenersStorage.gameListeners[i] is IGameFixedUpdateListener fixedUpdateListener)
                {
                    fixedUpdateListener.OnFixedUpdate();
                }
            }
        }

        public void Tick()
        {
            if (!CanUpdate()) return;

            for (int i = 0; i < this.listenersStorage.gameListeners.Count; i++)
            {
                if (this.listenersStorage.gameListeners[i] is IGameUpdateListener updateListener)
                {
                    updateListener.OnUpdate();
                }
            }
        }
    }
}