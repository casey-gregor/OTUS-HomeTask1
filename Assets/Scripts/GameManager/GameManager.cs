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

        [Inject]
        private void Construct(ListenersStorage listenersStorage)
        {
            this.listenersStorage = listenersStorage;
        }

        private void SwitchState(State state)
        {
            switch (state)
            {
                case State.Start:
                    for (int i = 0; i < this.listenersStorage.Listeners.Count; i++)
                    {
                        if (this.listenersStorage.Listeners[i] is IGameStartListener listener)
                        {
                            listener.OnStart();
                        }
                    }
                    break;
                case State.Pause:
                    for (int i = 0; i < this.listenersStorage.Listeners.Count; i++)
                    {
                        if (this.listenersStorage.Listeners[i] is IGamePauseListener listener)
                        {
                            listener.OnPause();
                        }
                    }
                    break;
                case State.Resume:
                    for (int i = 0; i < this.listenersStorage.Listeners.Count; i++)
                    {
                        if (this.listenersStorage.Listeners[i] is IGameResumeListener listener)
                        {
                            listener.OnResume();
                        }
                    }
                    break;
                case State.Finish:
                    for (int i = 0; i < this.listenersStorage.Listeners.Count; i++)
                    {
                        if (this.listenersStorage.Listeners[i] is IGameFinishListener listener)
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

        public void FixedTick()
        {
            if (!CanUpdate()) return;

            for (int i = 0; i < this.listenersStorage.Listeners.Count; i++)
            {

                if (this.listenersStorage.Listeners[i] is IGameFixedUpdateListener fixedUpdateListener)
                {
                    fixedUpdateListener.OnFixedUpdate();
                }
            }
        }

        public void Tick()
        {
            if (!CanUpdate()) return;

            for (int i = 0; i < this.listenersStorage.Listeners.Count; i++)
            {
                if (this.listenersStorage.Listeners[i] is IGameUpdateListener updateListener)
                {
                    updateListener.OnUpdate();
                }
            }
        }
    }
}